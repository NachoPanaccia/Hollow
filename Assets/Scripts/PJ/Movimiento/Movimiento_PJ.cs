using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_PJ : MonoBehaviour
{
    private IPlayerState currentState;
    private Animator anim;
    private float speed = 11f;
    private Caretaker caretaker;

    private int animationState = 0;

    public class Memento
    {
        public Vector3 Position { get; private set; }
        public IPlayerState PlayerState { get; private set; }

        public Memento(Vector3 position, IPlayerState playerState)
        {
        Position = position;
        PlayerState = playerState;
        }
    }

    public class Caretaker
    {
        private List<Memento> mementoList = new List<Memento>();

        public void Save(Memento memento)
        {
        mementoList.Add(memento);
        }

        public Memento Load(int index)
        {
        if (index < mementoList.Count)
            return mementoList[index];
        return null;
        }

        public int GetSaveCount()
        {
        return mementoList.Count;
        }
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        caretaker = new Caretaker();
        TransitionToState(new IdleState());
    }

    void Update()
    {
        currentState.UpdateState(this);
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveState();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (caretaker.GetSaveCount() > 0)
            {
                RestoreState(caretaker.GetSaveCount() - 1);
            }
        }
    }

    public void TransitionToState(IPlayerState newState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = newState;
        currentState.EnterState();
    }

    public void Move(Vector3 direction)
    {
        if (animationState != 1)
        {
            animationState = 1;
            anim.SetInteger("State", 1);
        }

        transform.position += direction * speed * Time.deltaTime;
    }

    public void StopMoving()
    {
        if (animationState != 0)
        {
            animationState = 0;
            anim.SetInteger("State", 0);
        }
    }

    public void SaveState()
    {
        Memento memento = new Memento(transform.position, currentState);
        caretaker.Save(memento);
    }

    public void RestoreState(int index)
    {
        Memento memento = caretaker.Load(index);
        if (memento != null)
        {
            transform.position = memento.Position;
            TransitionToState(memento.PlayerState);
        }
    }
}