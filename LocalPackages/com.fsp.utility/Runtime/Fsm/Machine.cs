namespace fsp.utility
{
    public class Machine<T>
    {
        private State<T> m_currentState = null;
        private State<T> m_previousState = null;

        public State<T> GetCurrentState()
        {
            return m_currentState;
        }

        public State<T> GetPreviousState()
        {
            return m_previousState;
        }

        public void SwitchToState(State<T> newState)
        {
            if (m_currentState != null)
            {
                m_previousState = m_currentState;
                m_currentState.Exit();
            }

            if (newState != null)
            {
                m_currentState = newState;
                m_currentState.Enter();
            }
        }

        public void SwitchToPreviousState()
        {
            if (m_previousState != null)
            {
                SwitchToState(m_previousState);
            }
        }
    }
}