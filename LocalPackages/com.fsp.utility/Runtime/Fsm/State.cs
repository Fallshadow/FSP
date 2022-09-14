namespace fsp.utility
{
    public class State<T>
    {
        protected Fsm<T> m_fsm;

        public void Init(Fsm<T> f)
        {
            m_fsm = f;
            onInit();
        }

        protected virtual void onInit()
        {
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void LateUpdate()
        {
        }

        public virtual int StateEnum()
        {
            return -1;
        }
    }
}