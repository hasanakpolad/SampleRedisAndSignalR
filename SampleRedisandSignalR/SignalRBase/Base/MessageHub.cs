using Microsoft.AspNetCore.SignalR;
using SampleRedisandSignalR.RedisBase.Logic;
using SampleRedisandSignalR.RedisBase.Models;
using System.Collections.Concurrent;

namespace SampleRedisandSignalR.SignalRBase.Base
{
    public class MessageHub : Hub
    {
        private IIndexManager _indexManager;
        public MessageHub(IIndexManager indexManager)
        {
            _indexManager = indexManager;
        }
        public static ConcurrentDictionary<string, UserModel> cacheUserList = new ConcurrentDictionary<string, UserModel>();

        public async Task JoinHubList(string connectionId, UserModel model)
        {
            var userModel = new UserModel()
            {
                Username = model.Username,
                Usid = model.Usid,
                ConnectionId = connectionId,
            };
            _indexManager.Set(model.Usid, userModel);
        }

        public async Task SendMessage(string user, string message)
        {
            var usr = _indexManager.Get<UserModel>(user);
            if (usr == null)
            {
                Console.Out.WriteLine("User not found.");
                return;
            }

            await Clients.Client(usr.ConnectionId).SendAsync("GetMessage", user, message);
            await Console.Out.WriteLineAsync(user + " : " + message);
        }
    }
}
