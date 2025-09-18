using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_PJ : MonoBehaviour
{
    private IPlayerState currentState;
    private Animator anim;
    private SimpleKinematicMover2D mover;

    private int animationState = 0;

    // ===== Memento =====
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
        public void Save(Memento m) => mementoList.Add(m);
        public Memento Load(int index) => (index < mementoList.Count) ? mementoList[index] : null;
        public int GetSaveCount() => mementoList.Count;
    }
    private Caretaker caretaker;

    void Start()
    {
        anim = GetComponent<Animator>();
        mover = GetComponent<SimpleKinematicMover2D>();
        if (!mover) mover = gameObject.AddComponent<SimpleKinematicMover2D>();

        caretaker = new Caretaker();
        TransitionToState(new IdleState());
    }

    void Update()
    {
        currentState.UpdateState(this);

        if (Input.GetKeyDown(KeyCode.E)) SaveState();
        if (Input.GetKeyDown(KeyCode.R) && caretaker.GetSaveCount() > 0) RestoreState(caretaker.GetSaveCount() - 1);
    }

    public void TransitionToState(IPlayerState newState)
    {
        if (currentState != null) currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    

    public void SetMoveInput(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.0001f)
        {
            if (animationState != 1)
            {
                animationState = 1;
                anim.SetInteger("State", 1);
            }
        }
        else
        {
            if (animationState != 0)
            {
                animationState = 0;
                anim.SetInteger("State", 0);
            }
        }

        mover.SetDirection(direction);
    }

    public void StopMoving()
    {
        animationState = 0;
        anim.SetInteger("State", 0);
        mover.Stop();
    }

    public void RotateTowardsMouse()
    {
        mover.LookAtPoint(Camera.main ? (Vector3)Camera.main.ScreenToWorldPoint(Input.mousePosition) : transform.position);
    }

    // ===== Memento =====
    public void SaveState()
    {
        var memento = new Memento(transform.position, currentState);
        caretaker.Save(memento);
    }

    public void RestoreState(int index)
    {
        var memento = caretaker.Load(index);
        if (memento != null)
        {
            transform.position = memento.Position;
            TransitionToState(memento.PlayerState);
        }
    }
}
