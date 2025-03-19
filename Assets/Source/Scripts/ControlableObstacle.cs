public class ControlableObstacle : Obstacle, IControlable
{
    public void PerfomAction()
    {
        Destroy(gameObject); 
    }
}
