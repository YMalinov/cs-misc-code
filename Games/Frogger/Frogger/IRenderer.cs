namespace Frogger
{
    interface IRenderer
    {
        void EnqueueForRendering(GameObject obj);

        void RenderAll(int lives, int score);

        void ClearQueue();
    }
}
