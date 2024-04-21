using DataClasses;
using Interfaces;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class Drawable : MonoBehaviour
    {
        public Color32[] currentPixelsColorArray { get; private set; }

        [field: SerializeField] public ObservableValue<float> percentageOfColoring { get; private set; } = new();

        [Header("Settings")]
        [SerializeField] private LayerMask _drawingLayers;
        [SerializeField] private Color _penColour;
        [SerializeField] private Color _unpaintedColour = Color.white;
        [SerializeField] private int _penWidth = 3;
        [SerializeField] private float _drawBetweenSpace = 2;
        [SerializeField] private float _minimumPixelTransparency = 0.9f;

        [Header("Components")]
        [SerializeField] private Transform _pen;

        private bool _isActive = false;

        private Sprite _originalDrawableSprite;
        private Texture2D _currentDrawableTexture;

        private Vector2 _previousDragPosition;

        private Rect _drawableSpriteRect;
        private Bounds _drawableSpriteBounds;

        [Inject] private IColorPicker _colorPicker;

        public async void Initialize(SpriteRenderer spriteRenderer)
        {
            _originalDrawableSprite = spriteRenderer.sprite;

            currentPixelsColorArray = _originalDrawableSprite.texture.GetPixels32();
            for (int i = 0; i < currentPixelsColorArray.Length; i++) { MarkPixelToChange(currentPixelsColorArray, i, _unpaintedColour); }

            _currentDrawableTexture = new Texture2D(_originalDrawableSprite.texture.width, _originalDrawableSprite.texture.height);
            _currentDrawableTexture.SetPixels32(currentPixelsColorArray);

            _currentDrawableTexture.Apply();

            _drawableSpriteRect = _originalDrawableSprite.rect;
            _drawableSpriteBounds = _originalDrawableSprite.bounds;

            spriteRenderer.sprite = Sprite.Create(_currentDrawableTexture, _drawableSpriteRect, new Vector2(0.5f, 0.5f));

            Material materialInstance = new Material(spriteRenderer.material);
            materialInstance.mainTexture = _currentDrawableTexture;
            spriteRenderer.material = materialInstance;

            _isActive = true;

            while (_isActive == true)
            {
                percentageOfColoring?.SetValue(await UpdateColorDifferencePercentage());
            }
        }

        private void OnEnable()
        {
            _colorPicker.onColorPicked += SetPenColor;
        }

        private void OnDisable()
        {
            _isActive = false;

            _colorPicker.onColorPicked -= SetPenColor;
        }

        private void Update()
        {
            if (_isActive == false) { return; }

            bool mouseDown = Input.GetMouseButton(0);

            if (mouseDown)
            {
                Vector2 mouseWorldPositioni = _pen.position;
                Collider2D hit = Physics2D.OverlapPoint(mouseWorldPositioni, _drawingLayers.value);

                if (hit != null && hit.transform != null)
                {
                    Vector3 transformLocalScale = transform.localScale;
                    Vector3 localPosition = transform.InverseTransformPoint(mouseWorldPositioni);

                    if (_penColour != IColorPicker.noColor) { Draw(transformLocalScale, localPosition); }
                }
                else
                {
                    _previousDragPosition = Vector2.zero;
                }
            }
            else if (mouseDown == false)
            {
                _previousDragPosition = Vector2.zero;
            }

            ApplyMarkedPixelChanges();
        }

        public void ApplyMarkedPixelChanges()
        {
            _currentDrawableTexture.SetPixels32(currentPixelsColorArray);
            _currentDrawableTexture.Apply();
        }

        public void Draw(Vector3 transformLocalScale, Vector3 localPosition)
        {
            Vector2 pixelPos = WorldToPixelCoordinates(transformLocalScale, localPosition);

            if (_previousDragPosition == Vector2.zero)
            {
                MarkPixelsToColour(pixelPos, _penWidth, _penColour);
            }
            else
            {
                ColourBetween(_previousDragPosition, pixelPos, _penWidth, _penColour);
            }

            _previousDragPosition = pixelPos;
        }

        public void ColourBetween(Vector2 start_point, Vector2 end_point, int width, Color color)
        {
            float distance = Vector2.Distance(start_point, end_point);
            Vector2 direction = (end_point - start_point).normalized;

            float stepSize = _drawBetweenSpace / distance;

            for (float lerp = 0; lerp <= 1; lerp += stepSize)
            {
                Vector2 cur_position = Vector2.Lerp(start_point, end_point, lerp);
                MarkPixelsToColour(cur_position, width, color);
            }
        }

        public void MarkPixelsToColour(Vector2 center_pixel, int pen_radius, Color color_of_pen)
        {
            int center_x = (int)center_pixel.x;
            int center_y = (int)center_pixel.y;

            for (int x = center_x - pen_radius; x <= center_x + pen_radius; x++)
            {
                if (x >= (int)_drawableSpriteRect.width || x < 0) { continue; };

                for (int y = center_y - pen_radius; y <= center_y + pen_radius; y++)
                {
                    if (y >= (int)_drawableSpriteRect.height || y < 0) { continue; };

                    if (Vector2.Distance(new Vector2(x, y), center_pixel) <= pen_radius)
                    {
                        MarkPixelToChange(x, y, color_of_pen);
                    }
                }
            }
        }

        public void MarkPixelToChange(int x, int y, Color color)
        {
            int array_pos = y * (int)_drawableSpriteRect.width + x;
            MarkPixelToChange(currentPixelsColorArray, array_pos, color);
        }

        public void MarkPixelToChange(Color32[] pixelArray, int arrayPosition, Color color)
        {
            if (arrayPosition > pixelArray.Length || arrayPosition < 0) { return; };
            if (pixelArray[arrayPosition].a < _minimumPixelTransparency) { return; };

            pixelArray[arrayPosition] = color;
        }

        public Vector2 WorldToPixelCoordinates(Vector3 transformLocalScale, Vector3 localPosition)
        {
            float pixelWidth = _drawableSpriteRect.width;
            float pixelHeight = _drawableSpriteRect.height;
            float unitsToPixels = pixelWidth / _drawableSpriteBounds.size.x * transformLocalScale.x;

            float centered_x = localPosition.x * unitsToPixels + pixelWidth / 2;
            float centered_y = localPosition.y * unitsToPixels + pixelHeight / 2;

            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_x), Mathf.RoundToInt(centered_y));

            return pixel_pos;
        }

        public int currentIndex = 0;

        private async Task<float> UpdateColorDifferencePercentage()
        {
            if (currentPixelsColorArray == null || currentPixelsColorArray == null)
            {
                Debug.LogError("Pixel arrays not initialized.");
                return 0;
            }

            Color32[] currentPixelsColorArray_OnlyNonTransparent = currentPixelsColorArray.Where(x => x.a > _minimumPixelTransparency).ToArray();

            int differentPixels = 0;

            await Task.Run(() =>
            {
                for (int i = 0; i < currentPixelsColorArray_OnlyNonTransparent.Length; i++)
                {
                    currentIndex = i;
                    if (_isActive == false) { return; }

                    Color originalPixel = currentPixelsColorArray_OnlyNonTransparent[i];

                    if (originalPixel != _unpaintedColour)
                    {
                        differentPixels++;
                    }
                }
            });

            return (float)differentPixels / (float)currentPixelsColorArray_OnlyNonTransparent.Length * 100f;
        }

        private void SetPenColor(Color color)
        {
            _penColour = color;
        }
    }
}