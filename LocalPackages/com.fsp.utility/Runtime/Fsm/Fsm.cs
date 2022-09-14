using System.Collections.Generic;
using fsp.debug;

namespace fsp.utility
{
    public class Fsm<T>
    {
        public T Owner { get; protected set; }

        private Machine<T> m_machine;
        private Dictionary<int, State<T>> m_stateMap = new Dictionary<int, State<T>>();

        public void Initialize(T owner)
        {
            Owner = owner;
            m_machine = new Machine<T>();
        }

        public void Finalize()
        {
            m_stateMap.Clear();
        }

        public void Update()
        {
            State<T> s = m_machine.GetCurrentState();
            if (s != null)
            {
                s.Update();
            }
        }

        public void FixedUpdate()
        {
            State<T> s = m_machine.GetCurrentState();
            if (s != null)
            {
                s.FixedUpdate();
            }
        }

        public void LateUpdate()
        {
            State<T> s = m_machine.GetCurrentState();
            if (s != null)
            {
                s.LateUpdate();
            }
        }

        public State<T> GetCurrentState()
        {
            if (m_machine == null)
            {
                return null;
            }

            return m_machine.GetCurrentState();
        }

        public void AddState(int stateEnum, State<T> state)
        {
            state.Init(this);
            if (!m_stateMap.ContainsKey(stateEnum))
            {
                m_stateMap[stateEnum] = state;
            }
        }

        public State<T> GetState(int stateEnum)
        {
            m_stateMap.TryGetValue(stateEnum, out State<T> s);
            return s;
        }

        public int GetCurrentStateEnum()
        {
            return GetStateEnum(GetCurrentState());
        }

        public int GetStateEnum(State<T> s)
        {
            if (m_stateMap.ContainsValue(s))
            {
                Dictionary<int, State<T>>.Enumerator iter = m_stateMap.GetEnumerator();
                while (iter.MoveNext())
                {
                    if (iter.Current.Value == s)
                    {
                        return iter.Current.Key;
                    }
                }
            }

            if (s != null) return s.StateEnum();

            PrintSystem.LogError($"要检查的state为空！！！");
            return 0;
        }

        public void SwitchToState(int stateEnum, bool isForce = false)
        {
            State<T> s = GetState(stateEnum);
            if (isForce || s != m_machine.GetCurrentState())
            {
                m_machine.SwitchToState(s);
            }
        }

        public void SwitchToState(State<T> s)
        {
            s.Init(this);
            if (s != m_machine.GetCurrentState())
            {
                m_machine.SwitchToState(s);
            }
        }

        public State<T> GetPreviousState()
        {
            return m_machine.GetPreviousState();
        }

        public Dictionary<int, State<T>>.Enumerator GetStateEnumerator()
        {
            return m_stateMap.GetEnumerator();
        }
    }
}