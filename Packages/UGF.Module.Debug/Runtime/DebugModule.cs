using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Logs.Runtime;
using UGF.Module.Assets.Runtime;

namespace UGF.Module.Debug.Runtime
{
    public class DebugModule : ApplicationModuleAsync<DebugModuleDescription>
    {
        public DebugUIComponentInstance DebugUIComponent { get; }
        public DebugGLComponentInstance DebugGLComponent { get; }

        protected IAssetModule AssetModule { get; }

        public DebugModule(DebugModuleDescription description, IApplication application) : base(description, application)
        {
            DebugUIComponent = new DebugUIComponentInstance(Application, Description.DebugUIGameObjectName);
            DebugGLComponent = new DebugGLComponentInstance(Application, Description.DebugGLGameObjectName);

            AssetModule = Application.GetModule<IAssetModule>();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (Description.DebugComponentLoadOnInitialize)
            {
                if (Description.HasDebugUIProviderId)
                {
                    Log.Debug("Debug module load UI provider.");

                    DebugGLComponent.Load(Description.DebugUIProviderId);
                }

                if (Description.HasDebugGLProviderId)
                {
                    Log.Debug("Debug module load GL provider.");

                    DebugGLComponent.Load(Description.DebugGLProviderId);
                }
            }
        }

        protected override async Task OnInitializeAsync()
        {
            await base.OnInitializeAsync();

            if (Description.DebugComponentLoadOnInitializeAsync)
            {
                if (!DebugUIComponent.IsLoaded && Description.HasDebugUIProviderId)
                {
                    Log.Debug("Debug module load UI provider.");

                    await DebugUIComponent.LoadAsync(Description.DebugUIProviderId);
                }

                if (!DebugGLComponent.IsLoaded && Description.HasDebugGLProviderId)
                {
                    Log.Debug("Debug module load GL provider.");

                    await DebugGLComponent.LoadAsync(Description.DebugGLProviderId);
                }
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (DebugUIComponent.IsLoaded)
            {
                Log.Debug("Debug module unload UI provider.");

                DebugUIComponent.Unload();
            }

            if (DebugGLComponent.IsLoaded)
            {
                Log.Debug("Debug module unload GL provider.");

                DebugGLComponent.Unload();
            }
        }
    }
}
