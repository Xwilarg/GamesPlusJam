﻿using GamesPlusJam.Action.Interaction;
using UnityEngine;

namespace GamesPlusJam.Puzzle
{
    public class UpDown : AInteraction
    {
        public override bool IsOneWay => true;
        public override bool IsAvailable => Available;

        public override void InteractOff(PlayerController pc)
        { }

        public override void InteractOn(PlayerController pc)
        {
            Manager.Activate(Index);
        }

        [SerializeField]
        private float _speed;

        [SerializeField]
        private Toggeable _previousPipe;

        private AudioSource _source;

        public void TogglePipeStatus(bool value)
        {
            //_previousPipe.Toggle(value);
        }

        private float _prog = 0f;
        public bool GoUp { private set; get; } =false;
        private Vector3 _orPos;

        public int Index { set; private get; }
        public bool Available { set; private get; } = true;
        public UpDownManager Manager { set; private get; }

        private void Update()
        {
            _prog = Mathf.Clamp01(_prog + Time.deltaTime * _speed * (GoUp ? 1f : -1f));
            transform.position = Vector3.Lerp(_orPos, _orPos + Vector3.down, _prog);
        }

        private void Start()
        {
            _orPos = transform.position;
            _source = GetComponent<AudioSource>();
        }

        public void Activate(bool bypassAudio = false)
        {
            GoUp = !GoUp;
            if (!bypassAudio)
            {
                _source.pitch = Random.Range(.75f, 1.25f);
                _source.Play();
            }
        }
    }
}
