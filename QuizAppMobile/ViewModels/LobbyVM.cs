﻿using FontAwesome;
using QuizAppMobile.Constants;
using QuizAppMobile.Models.SignalR;
using QuizAppMobile.Services.Implementations;
using QuizAppMobile.Services.Interfaces;
using QuizAppMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuizAppMobile.ViewModels
{
    public class LobbyVM : BindableObject
    {
        private string _totalTime;

        private readonly LobbyConnection _lobbyConnection;

        private readonly IMessageService _messageService;

        public LobbyVM()
        {
            _messageService = DependencyService.Resolve<IMessageService>();

            _lobbyConnection = new LobbyConnection
            (
                InitializeUsers,
                AddUser,
                new Action(async () => await CloseLobby()),
                new Action(async () => await Kick()),
                RedirectToQuiz
            );

            UserCollection = new ObservableCollection<View>();
        }

        public string LobbyCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public double Rating { get; set; }

        public int TotalQuestions { get; set; }

        public string TotalTime 
        {
            get
            {
                return $"{_totalTime}s";
            }
            set
            {
                _totalTime = value;
            } 
        }

        public double TotalPoints { get; set; }

        public StackLayout RatingBox { get; set; }

        public StackLayout UserBox { get; set; }

        public ObservableCollection<View> UserCollection { get; set; }

        public void RenderRatings()
        {
            var filledStars = Rating;
            var emptyStars = 5 - filledStars;

            for (int i = 0; i < filledStars; i++)
            {
                var filledStarLabel = new Label
                {
                    Text = FontAwesomeIcons.Star,
                    FontFamily = Device.RuntimePlatform == Device.Android ? AndroidFonts.Solid : string.Empty
                };
                RatingBox.Children.Add(filledStarLabel);
            }
            for (int i = 0; i < emptyStars; i++)
            {
                var emptyStarLabel = new Label
                {
                    Text = FontAwesomeIcons.Star,
                    FontFamily = Device.RuntimePlatform == Device.Android ? AndroidFonts.Regular : string.Empty
                };
                RatingBox.Children.Add(emptyStarLabel);
            }
        }

        public async Task StartConnection()
        {
            if (Application.Current.Properties.TryGetValue(Properties.UserId, out var userId))
            {
                await _lobbyConnection.Connect(LobbyCode, (string)userId);
            }
            return;
        }

        private void AddUser(string username)
        {

        }

        private void InitializeUsers(Lobby lobby)
        {
            //var childList = new List<View>();
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };
            //childList.Add(GetHostStackLayout(lobby.OwnerUsername));
            stackLayout.Children.Add(GetHostStackLayout(lobby.OwnerUsername));
            foreach (var user in lobby.ConnectedUsers)
                stackLayout.Children.Add(GetUserLabel(user));
            //childList.Add(GetUserLabel(user));
            UserBox.Children.Add(stackLayout);
            //foreach (var component in childList)
                //UserBox.Children.Add(component);
        }

        private Label GetUserLabel (string username)
        {
            var usernameLabel = new Label
            {
                Text = username,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            return usernameLabel;
        }

        private StackLayout GetHostStackLayout(string username)
        {
            var stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };
            var crownIcon = new Label
            {
                Text = FontAwesomeIcons.Crown,
                FontFamily = Device.RuntimePlatform == Device.Android ? AndroidFonts.Solid : string.Empty,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            var usernameLabel = new Label
            {
                Text = username,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            stackLayout.Children.Add(crownIcon);
            stackLayout.Children.Add(usernameLabel);
            return stackLayout;
        }

        private async Task Kick()
        {
            await DisconectFromLobby("You have been kicked from the lobby");
        }

        private void RedirectToQuiz(string url)
        {

        }

        private async Task CloseLobby()
        {
            await DisconectFromLobby("The host has closed this lobby");
        }

        private async Task DisconectFromLobby(string message)
        {
            await _lobbyConnection.Disconnect();
            await _messageService.DisplayErrorMessage(message);         
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
