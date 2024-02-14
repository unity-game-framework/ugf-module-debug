using UGF.Application.Runtime;
using UGF.DebugTools.Runtime;

namespace UGF.Module.Debug.Runtime
{
    public class DebugGLComponentInstance : DebugComponentInstance<DebugGLProviderAsset, DebugGLComponent>
    {
        public DebugGLComponentInstance(IApplication application, string gameObjectName) : base(application, gameObjectName)
        {
        }

        protected override DebugGLComponent OnCreateComponent(DebugGLProviderAsset asset)
        {
            DebugGLComponent component = base.OnCreateComponent(asset);

            component.Provider = asset;

            return component;
        }
    }
}
