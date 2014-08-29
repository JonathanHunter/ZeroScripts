﻿//written by: Jonathan Hunter
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Menus
{
    class KeyboardControls : MonoBehaviour
    {
        public Texture Title, CursorPic;
        public GUIStyle LabelStyle;
        private enum State
        {
            Attack = 0, Jump, Dash, Pause, Accept,
            Cancel, Up, Down, Left, Right, Default, Exit, GettingKey
        };
        private int cursor, oldCursor;
        private KeyCode temp = 0;
        private bool wait = false, updateSkip = false;
        void Start()
        {
            cursor = (int)State.Attack;
        }
        void Update()
        {
            if (updateSkip)
            {
                updateSkip = false;
                return;
            }
            if (cursor != (int)State.GettingKey &&
                (CustomInput.CancelUp || (cursor == (int)State.Exit &&
                CustomInput.AcceptUp)))
                Destroy(this.gameObject);
            else if (cursor == (int)State.Default && CustomInput.AcceptUp)
                CustomInput.Default();
            else if (CustomInput.AcceptUp)
            {
                oldCursor = cursor;
                cursor = (int)State.GettingKey;
            }
            else
            {
                if (CustomInput.DownUp)
                {
                    if (cursor == (int)State.Exit)
                        cursor = (int)State.Attack;
                    else
                        cursor++;
                }
                if (CustomInput.UpUp)
                {
                    if (cursor == (int)State.Attack)
                        cursor = (int)State.Exit;
                    else
                        cursor--;
                }
            }
        }
        private void GetNewKey()
        {
            if (!wait && !Input.GetKey(KeyCode.Escape))
            {
                temp = Event.current.keyCode;
                GUI.Label(new Rect(Screen.width * (7f / 19f),
                    Screen.height * (6f / 12f), Screen.width * (6f / 19f),
                    Screen.height * (3f / 12f)),
                    "Press the new key you want to use, Escape fo cancel."
                    , LabelStyle);
                if (temp != 0)
                {
                    if (oldCursor == (int)State.Attack)
                        CustomInput.KeyBoardAttack = temp;
                    else if (oldCursor == (int)State.Jump)
                        CustomInput.KeyBoardJump = temp;
                    else if (oldCursor == (int)State.Dash)
                        CustomInput.KeyBoardDash = temp;
                    else if (oldCursor == (int)State.Up)
                        CustomInput.KeyBoardUp = temp;
                    else if (oldCursor == (int)State.Down)
                        CustomInput.KeyBoardDown = temp;
                    else if (oldCursor == (int)State.Left)
                        CustomInput.KeyBoardLeft = temp;
                    else if (oldCursor == (int)State.Right)
                        CustomInput.KeyBoardRight = temp;
                    else if (oldCursor == (int)State.Accept)
                        CustomInput.KeyBoardAccept = temp;
                    else if (oldCursor == (int)State.Cancel)
                        CustomInput.KeyBoardCancel = temp;
                    else
                        CustomInput.KeyBoardPause = temp;
                    wait = true;
                }
            }
            else if (!wait)
                wait = true;
            else
            {
                updateSkip = true;
                if (!Input.anyKey)
                {
                    cursor = oldCursor;
                    wait = false;
                }
            }
        }
        void OnGUI()//values based off of 19x12 grid
        {
            //left, top, 
            //width, height
            //title pic
            GUI.DrawTexture(new Rect(
                Screen.width * (6f / 19f), Screen.height * (1f / 12f),
                Screen.width * (7f / 19f), Screen.height * (2f / 12f)),
                Title);
            drawButtons();
            drawLabels();
            drawCursor();
            if (cursor == (int)State.GettingKey)
                GetNewKey();
        }
        private void drawButtons()
        {
            //left, top,
            //width, height
            //label
            //state, style
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (4f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardAttack.ToString(),
                State.Attack, LabelStyle);
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (5f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardJump.ToString(),
                State.Jump, LabelStyle);
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (6f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardDash.ToString(),
                State.Dash, LabelStyle);
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (7f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardPause.ToString(),
                State.Pause, LabelStyle);
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (8f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardAccept + "/Enter",
                State.Accept, LabelStyle);
            makeButton(new Rect(
                Screen.width * (7f / 19f), Screen.height * (9f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardCancel + "/Escape",
                State.Cancel, LabelStyle);
            makeButton(new Rect(
                Screen.width * (12f / 19f), Screen.height * (4f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardUp + "/Up arrow",
                State.Up, LabelStyle);
            makeButton(new Rect(
                Screen.width * (12f / 19f), Screen.height * (5f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardDown + "/Down arrow",
                State.Down, LabelStyle);
            makeButton(new Rect(
                Screen.width * (12f / 19f), Screen.height * (6f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardLeft + "/Left arrow",
                State.Left, LabelStyle);
            makeButton(new Rect(
                Screen.width * (12f / 19f), Screen.height * (7f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                CustomInput.KeyBoardRight + "/Right arrow",
                State.Right, LabelStyle);
            makeButton(new Rect(
               Screen.width * (9f / 19f), Screen.height * (10f / 12f),
               Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
               "Defaults",
               State.Default, LabelStyle);
            makeButton(new Rect(
                Screen.width * (12f / 19f), Screen.height * (10f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Exit",
                State.Exit, LabelStyle);
        }
        private void makeButton(Rect position, string label,
            State cursorStateToReturnTo, GUIStyle style)
        {
            if (cursorStateToReturnTo == State.Default)
            {
                if (GUI.Button(position, label, style))
                {
                    CustomInput.Default();
                    cursor = (int)State.Default;
                }
            }
            else if (cursorStateToReturnTo == State.Exit)
            {
                if (GUI.Button(position, label, style))
                    Destroy(this.gameObject);
            }
            else
            {
                if (GUI.Button(position, label, style))
                {
                    oldCursor = (int)cursorStateToReturnTo;
                    cursor = (int)State.GettingKey;
                }
            }
        }
        private void drawLabels()
        {
            //left, top, 
            //width, height
            //menu title
            GUI.Label(new Rect(
                Screen.width * (7f / 19f), Screen.height * (3f / 12f),
                Screen.width * (4f / 19f), Screen.height * (1f / 12f)),
                "Keyboard", LabelStyle);
            //keys
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (4f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Attack", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (5f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Jump", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (6f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Dash", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (7f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Pause", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (8f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Accept", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (4f / 19f), Screen.height * (9f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Cancel", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (9f / 19f), Screen.height * (4f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Up", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (9f / 19f), Screen.height * (5f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Down", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (9f / 19f), Screen.height * (6f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Left", LabelStyle);
            GUI.Label(new Rect(
                Screen.width * (9f / 19f), Screen.height * (7f / 12f),
                Screen.width * (2f / 19f), Screen.height * (1f / 12f)),
                "Right", LabelStyle);
        }
        private void drawCursor()
        {
            //left, top, width, height
            if (cursor == (int)State.Attack)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (4f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Jump)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Dash)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (6f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Up)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (11f / 19f), Screen.height * (4f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Down)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (11f / 19f), Screen.height * (5f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Left)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (11f / 19f), Screen.height * (6f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Right)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (11f / 19f), Screen.height * (7f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Accept)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (8f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Cancel)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (9f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Pause)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (6f / 19f), Screen.height * (7f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Default)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (8f / 19f), Screen.height * (10f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
            else if (cursor == (int)State.Exit)
            {
                GUI.DrawTexture(new Rect(
                    Screen.width * (11f / 19f), Screen.height * (10f / 12f),
                    Screen.width * (1f / 19f), Screen.height * (1f / 12f)),
                    CursorPic);
            }
        }
    }
}
