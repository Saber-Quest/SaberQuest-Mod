using SaberQuest.Providers.ApiProvider;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SaberQuest.Stores
{
	internal class Store<T> where T : Store<T>
	{
		private static T Instance { get; set; }
		protected ISaberQuestApiProvider ApiProvider { get; private set; }
		protected SiraLog Logger { get; private set; }

		[Inject]
		private void Construct(ISaberQuestApiProvider apiProvider, SiraLog logger)
		{
			Instance = (T)this;
			ApiProvider = apiProvider;
			Logger = logger;
		}

		public static T Get()
		{
			if(Instance == null)
			{
				Console.Error.WriteLine($"Failed to get {typeof(T).Name} as it has not been constructed yet!");
			}
			return Instance;
		}
	}
}
