using System;
using System.Text;

namespace Frogger
{
    class ConsoleRenderer : IRenderer
    {
        public int renderContextMatrixRows { get; private set; }
        public int renderContextMatrixCols { get; private set; }
        char[,] renderContextMatrix;

        public ConsoleRenderer(int visibleConsoleRows, int visibleConsoleCols)
        {
            renderContextMatrix = new char[visibleConsoleRows, visibleConsoleCols];

            this.renderContextMatrixRows = renderContextMatrix.GetLength(0);
            this.renderContextMatrixCols = renderContextMatrix.GetLength(1);

            this.ClearQueue();
        }

        public void EnqueueForRendering(GameObject obj)
        {
            char[] objImage = obj.GetImage();

            int imageCols = objImage.Length;

            Coordinates objTopLeft = obj.GetTopLeft();

            int lastCol = Math.Min(objTopLeft.Col + imageCols, this.renderContextMatrixCols);

            for (int col = obj.GetTopLeft().Col; col < lastCol; col++)
            {
                if (col >= 0 && col < renderContextMatrixCols)
                {
                    renderContextMatrix[objTopLeft.Row, col] = objImage[col - obj.GetTopLeft().Col];
                }
            }
        }

        public void RenderAll(int lives = 0, int score = 0)
        {
            StringBuilder scene = new StringBuilder();

            string scoreText = "Score: " + score;
            string lifeText = "Lives: " + lives;
            scene.Append(scoreText + new string(' ', Console.WindowWidth - 1 - scoreText.Length - lifeText.Length) + lifeText + '\n');

            for (int row = 0; row < this.renderContextMatrixRows; row++)
            {
                for (int col = 0; col < this.renderContextMatrixCols; col++)
                {
                    scene.Append(this.renderContextMatrix[row, col]);
                }

                scene.Append('\n');
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(scene.ToString());
        }

        public void ClearQueue()
        {
            for (int row = 0; row < this.renderContextMatrixRows; row++)
            {
                for (int col = 0; col < this.renderContextMatrixCols; col++)
                {
                    this.renderContextMatrix[row, col] = ' ';
                }
            }
        }
    }
}