using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public class SignalRConnection
{
    private const string HubAddress = "https://bigprojectapi-500201348.azurewebsites.net/chessgpthub";

    private HubConnection _connection;

    public event Action<string> OnMessageReceived;

    public async void Initialize()
    {
        _connection = new HubConnectionBuilder()
            .WithUrl(HubAddress)
            .Build();

        _connection.On("ReceiveMessage", (string message) => {
            OnMessageReceived?.Invoke(message);
        });

       await _connection.StartAsync(); 
    }

    public async Task SendMessage(string message)
    {
        await _connection.SendAsync("SendMessage", message);
    }
}
