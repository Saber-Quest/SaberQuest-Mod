using Newtonsoft.Json;

namespace SaberQuest.Models.SaberQuest.Websocket
{
	internal class PacketHandler
	{
		public string MakePacket(string eventName, string data)
		{
			var packet = new PacketModel
			{
				EventName = eventName,
				JsonPayLoad = data
			};

			return JsonConvert.SerializeObject(packet);
		}

		public PacketModel OpenPacket(string data) => JsonConvert.DeserializeObject<PacketModel>(data);
	}
}
