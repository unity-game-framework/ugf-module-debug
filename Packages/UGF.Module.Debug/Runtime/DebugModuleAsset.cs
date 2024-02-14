using UGF.Application.Runtime;
using UGF.DebugTools.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.Module.Debug.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug Module", order = 2000)]
    public class DebugModuleAsset : ApplicationModuleAsset<DebugModule, DebugModuleDescription>
    {
        [SerializeField] private bool m_debugComponentLoadOnInitialize;
        [SerializeField] private bool m_debugComponentLoadOnInitializeAsync;
        [SerializeField] private string m_debugUIGameObjectName = "DebugUI";
        [SerializeField] private string m_debugGLGameObjectName = "DebugGL";
        [AssetId(typeof(DebugUIProviderAsset))]
        [SerializeField] private GlobalId m_debugUIProvider;
        [AssetId(typeof(DebugGLProviderAsset))]
        [SerializeField] private GlobalId m_debugGLProvider;

        public bool DebugComponentLoadOnInitialize { get { return m_debugComponentLoadOnInitialize; } set { m_debugComponentLoadOnInitialize = value; } }
        public bool DebugComponentLoadOnInitializeAsync { get { return m_debugComponentLoadOnInitializeAsync; } set { m_debugComponentLoadOnInitializeAsync = value; } }
        public string DebugUIGameObjectName { get { return m_debugUIGameObjectName; } set { m_debugUIGameObjectName = value; } }
        public string DebugGLGameObjectName { get { return m_debugGLGameObjectName; } set { m_debugGLGameObjectName = value; } }
        public GlobalId DebugUIProvider { get { return m_debugUIProvider; } set { m_debugUIProvider = value; } }
        public GlobalId DebugGLProvider { get { return m_debugGLProvider; } set { m_debugGLProvider = value; } }

        protected override IApplicationModuleDescription OnBuildDescription()
        {
            return new DebugModuleDescription(
                typeof(DebugModule),
                m_debugComponentLoadOnInitialize,
                m_debugComponentLoadOnInitializeAsync,
                m_debugUIGameObjectName,
                m_debugGLGameObjectName,
                m_debugUIProvider.IsValid() ? m_debugUIProvider : null,
                m_debugGLProvider.IsValid() ? m_debugGLProvider : null
            );
        }

        protected override DebugModule OnBuild(DebugModuleDescription description, IApplication application)
        {
            return new DebugModule(description, application);
        }
    }
}
