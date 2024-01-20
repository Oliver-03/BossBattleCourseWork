using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace BossBattleCourseWork
{
    // This shape batcher was taken from the lab
    public class ShapeBatcher
    {
        private Game1 _game;

        private bool _disposed;
        private BasicEffect _effect;
        private VertexPositionColor[] _vertices;
        private int[] _indices;

        private int _shapeCount = 0;
        private int _vertexCount = 0;
        private int _indexCount = 0;

        private bool _isStarted = false;

        public static readonly float MIN_LINE_THICKNESS = 2f;
        public static readonly float MAX_LINE_THICKNESS = 10f;

        public ShapeBatcher(Game1 pGame)
        {
            _game = pGame ?? throw new ArgumentNullException(nameof(pGame));
            _disposed = false;
            _effect = new BasicEffect(_game.GraphicsDevice);
            _effect.TextureEnabled = false;
            _effect.FogEnabled = false;
            _effect.LightingEnabled = false;
            _effect.VertexColorEnabled = true;
            _effect.World = Matrix.Identity;
            _effect.View = Matrix.Identity;
            _effect.Projection = Matrix.Identity;

            const int MAX_VERTEX_COUNT = 1024;
            const int MAX_INDEX_COUNT = MAX_VERTEX_COUNT * 3;
            _vertices = new VertexPositionColor[MAX_VERTEX_COUNT];
            _indices = new int[MAX_INDEX_COUNT];
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _effect?.Dispose();
            _disposed = true;
        }

        public void Begin()
        {
            if (_isStarted)
            {
                throw new System.Exception("Batch already started.");
            }

            Viewport viewport = _game.GraphicsDevice.Viewport;
            _effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0f, 1f);

            _isStarted = true;
        }

        public void End()
        {
            Flush();
            _isStarted = false;
        }

        private void Flush()
        {
            if (_shapeCount == 0)
            {
                return;
            }

            EnsureStarted();

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList,
                    _vertices,
                    0,
                    _vertexCount,
                    _indices,
                    0,
                    _indexCount / 3
                );
            }

            _shapeCount = 0;
            _indexCount = 0;
            _vertexCount = 0;
        }

        private void EnsureStarted()
        {
            if (!_isStarted)
            {
                throw new System.Exception("Batch not started.");
            }
        }

        private void EnsureSpace(int pShapeVertexCount, int pShapeIndexCount)
        {
            if (pShapeVertexCount > _vertices.Length)
            {
                throw new System.Exception("Maximum shape vertex count is " + _vertices.Length);
            }

            if (pShapeIndexCount > _indices.Length)
            {
                throw new System.Exception("Maximum shape index count is " + _indices.Length);
            }

            if (_vertexCount + pShapeVertexCount > _vertices.Length ||
                _indexCount + pShapeIndexCount > _indices.Length)
            {
                Flush();
            }
        }

        /// <summary>
        /// Draws a line from pA to PB as a rectangle with thickness pThickness and colour pColour.
        /// </summary>
        /// <param name="pA">Start of the line</param>
        /// <param name="pB">End of the line</param>
        /// <param name="pThickness">Thickness of the line (clamped between 2 and 10)</param>
        /// <param name="pColour">Colour of the line</param>
        public void DrawLine(Vector2 pA, Vector2 pB, float pThickness, Color pColour)
        {
            EnsureStarted();

            const int shapeVertexCount = 4;
            const int shapeIndexCount = 6;

            EnsureSpace(shapeVertexCount, shapeIndexCount);

            pThickness = Math.Clamp(pThickness, MIN_LINE_THICKNESS, MAX_LINE_THICKNESS);

            float halfThickness = pThickness * 0.5f;

            float e1x = pB.X - pA.X;
            float e1y = pB.Y - pA.Y;

            float invLength = halfThickness / MathF.Sqrt(e1x * e1x + e1y * e1y);

            e1x *= invLength;
            e1y *= invLength;

            float e2x = -e1x;
            float e2y = -e1y;

            float n1x = -e1y;
            float n1y = e1x;

            float n2x = -n1x;
            float n2y = -n1y;

            _indices[_indexCount++] = 0 + _vertexCount;
            _indices[_indexCount++] = 1 + _vertexCount;
            _indices[_indexCount++] = 2 + _vertexCount;
            _indices[_indexCount++] = 0 + _vertexCount;
            _indices[_indexCount++] = 2 + _vertexCount;
            _indices[_indexCount++] = 3 + _vertexCount;

            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pA.X + n1x + e2x, pA.Y + n1y + e2y, 0f), pColour);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pB.X + n1x + e1x, pB.Y + n1y + e1y, 0f), pColour);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pB.X + n2x + e1x, pB.Y + n2y + e1y, 0f), pColour);
            _vertices[_vertexCount++] = new VertexPositionColor(new Vector3(pA.X + n2x + e2x, pA.Y + n2y + e2y, 0f), pColour);

            _shapeCount++;
        }

        public void DrawCircle(Vector2 pCentre, float pRadius, int pNumVertices, float pThickness, Color pColour)
        {
            const int MIN_POINTS = 3;
            const int MAX_POINTS = 256;

            pNumVertices = Math.Clamp(pNumVertices, MIN_POINTS, MAX_POINTS);

            float deltaAngle = MathHelper.TwoPi / pNumVertices;
            float angle = 0f;

            for (int i = 0; i < pNumVertices; i++)
            {
                float ax = pCentre.X + pRadius * MathF.Sin(angle);
                float ay = pCentre.Y + pRadius * MathF.Cos(angle);

                angle += deltaAngle;

                float bx = pCentre.X + pRadius * MathF.Sin(angle);
                float by = pCentre.Y + pRadius * MathF.Cos(angle);
                DrawLine(new Vector2(ax, ay), new Vector2(bx, by), pThickness, pColour);
            }
        }

        public void DrawArrow(Vector2 pStart, Vector2 pVector, float pThickness, float pArrowSize, Color pColour)
        {
            Vector2 lineEnd = pStart + pVector;

            Vector2 u = pVector * (1f / pVector.Length());
            Vector2 v = new Vector2(-u.Y, u.X);

            Vector2 arrowHead1 = lineEnd - pArrowSize * u + pArrowSize * v;
            Vector2 arrowHead2 = lineEnd - pArrowSize * u - pArrowSize * v;

            DrawLine(pStart, lineEnd, pThickness, pColour);
            DrawLine(lineEnd, arrowHead1, pThickness, pColour);
            DrawLine(lineEnd, arrowHead2, pThickness, pColour);
        }
    }
}

