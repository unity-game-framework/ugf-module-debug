using UGF.Application.Runtime;
using UGF.DebugTools.Runtime.UI;

namespace UGF.Module.Debug.Runtime
{
    public class DebugUIComponentInstance : DebugComponentInstance<DebugUIProviderAsset, DebugUIComponent>
    {
        public DebugUIComponentInstance(IApplication application, string gameObjectName) : base(application, gameObjectName)
        {
        }

        protected override DebugUIComponent OnCreateComponent(DebugUIProviderAsset asset)
        {
            DebugUIComponent component = base.OnCreateComponent(asset);

            component.Provider = asset;

            return component;
        }
    }
}
