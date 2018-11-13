using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System.Collections.Generic;
using System;
using Android.Views;
using Android.Views.InputMethods;
using Android.Content;
using Android.Graphics;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public int TurnCounter { get; set; } = 1;
        public TextView TurnTextView { get; set; }
        private Button AddButton { get; set; }
        private Button StartButton { get; set; }
        private Button PreviousButton { get; set; }
        private Button NextButton { get; set; }
        private EditText NameInput { get; set; }
        private List<string> Players { get; set; } = new List<string>();
        private List<TextView> PlayersTextView { get; set; } = new List<TextView>();
        private List<TextView> Phases { get; set; } = new List<TextView>();
        private int PlayerCount { get; set; } = 0;
        private int _activePlayer = 1;

        public int ActivePlayer
        {
            get { return _activePlayer; }
            set {
                if (value > Players.Count)
                {
                    _activePlayer = 1;
                }
                else if (value < 1)
                {
                    _activePlayer = Players.Count;
                }
                else
                {
                    _activePlayer = value;
                }
                
            }
        }

        private int _activephase = 1;
        public int ActivePhase
        {
            get { return _activephase; }
            set
            {
                if (value > 3)
                {
                    _activephase = 1;
                    ActivePlayer++;
                    TurnCounter++;

                }
                else if (value < 1)
                {
                    _activephase = 3;
                    ActivePlayer--;
                    TurnCounter--;
                }
                else
                {
                    _activephase = value;
                }
            }
        }
        public TextView Phase1 { get; set; }
        public TextView Phase2 { get; set; }
        public TextView Phase3 { get; set; }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            AddButton = FindViewById<Button>(Resource.Id.add);
            AddButton.Click += AddPlayer;

            StartButton = FindViewById<Button>(Resource.Id.start);
            StartButton.Click += StartGame;

            PreviousButton = FindViewById<Button>(Resource.Id.pre);
            PreviousButton.Click += PreviousPhase;

            NextButton = FindViewById<Button>(Resource.Id.next);
            NextButton.Click += NextPhase;

            NameInput = FindViewById<EditText>(Resource.Id.name);

            Phase1 = FindViewById<TextView>(Resource.Id.phase1);
            Phases.Add(Phase1);
            Phase2 = FindViewById<TextView>(Resource.Id.phase2);
            Phases.Add(Phase2);
            Phase3 = FindViewById<TextView>(Resource.Id.phase3);
            Phases.Add(Phase3);

            TurnTextView = FindViewById<TextView>(Resource.Id.turn);

            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player1));
            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player2));
            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player3));
            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player4));
            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player5));
            PlayersTextView.Add(FindViewById<TextView>(Resource.Id.player6));
        }

        public void ShowGame()
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (i == ActivePlayer-1)
                {
                    PlayersTextView[i].SetBackgroundColor(Color.Yellow);
                }
                else
                {
                    PlayersTextView[i].SetBackgroundColor(Color.White);
                }
            }
            for (int i = 1; i < 4; i++)
            {
                if (ActivePhase == i)
                {
                    Phases[i-1].SetBackgroundColor(Color.Yellow);
                }
                else
                {
                    Phases[i-1].SetBackgroundColor(Color.White);
                }
            }
            TurnTextView.Text = $"Turn: {TurnCounter.ToString()}";
        }

        private void NextPhase(object sender, EventArgs e)
        {
            ActivePhase++;
            ShowGame();
        }

        private void PreviousPhase(object sender, EventArgs e)
        {
            if (TurnCounter == 1 && ActivePhase == 1)
            {
            }
            else
            {
                ActivePhase--;
                ShowGame();
            }
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (PlayerCount > 1)
            {
            AddButton.Visibility = ViewStates.Invisible;
            StartButton.Visibility = ViewStates.Invisible;
            NameInput.Visibility = ViewStates.Invisible;
            Phase1.Visibility = ViewStates.Visible;
            Phase2.Visibility = ViewStates.Visible;
            Phase3.Visibility = ViewStates.Visible;
            PreviousButton.Visibility = ViewStates.Visible;
            NextButton.Visibility = ViewStates.Visible;
            TurnTextView.Visibility = ViewStates.Visible;
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(NameInput.WindowToken, 0);
            ShowGame();
            }
        }

        private void AddPlayer(object sender, EventArgs e)
        {
            if (PlayerCount >= 6 || NameInput.Text == "")
            {
                return;
            }

            Players.Add(NameInput.Text);
            NameInput.Text = "";
            PlayerCount++;

            if (PlayerCount == 6)
            {
                AddButton.Visibility = ViewStates.Invisible;
            }
            ShowPlayer();
        }

        private void ShowPlayer()
        {
            var name = $"player{PlayerCount}";
            TextView textField = null;
            textField = PlayersTextView[PlayerCount - 1];
            textField.Text = Players[PlayerCount - 1];
        }
    }
}