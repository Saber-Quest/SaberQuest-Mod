using IPA;
using IPA.Config;
using SiraUtil.Zenject;
using IPALogger = IPA.Logging.Logger;

namespace SaberQuest
{
	[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
	public class Plugin
	{
		[Init]
		public void Init(IPALogger logger, Config config, Zenjector zenjector)
		{
			zenjector.UseSiraSync();
			zenjector.UseLogger(logger);
			zenjector.UseMetadataBinder<Plugin>();
		}
	}
}