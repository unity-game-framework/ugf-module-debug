using System;
using System.Threading.Tasks;
using UGF.Application.Runtime;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.Module.Assets.Runtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.Module.Debug.Runtime
{
    public abstract class DebugComponentInstance<TAsset, TComponent>
        where TAsset : BuilderAssetBase
        where TComponent : Component
    {
        public IApplication Application { get; }
        public string GameObjectName { get; }
        public bool IsLoaded { get { return m_state; } }
        public GlobalId AssetId { get { return m_assetId ?? throw new ArgumentException("Value not specified."); } }
        public TAsset Asset { get { return m_asset ? m_asset : throw new ArgumentException("Value not specified."); } }
        public TComponent Component { get { return m_component ? m_component : throw new ArgumentException("Value not specified."); } }

        protected IAssetModule AssetModule { get; }

        private InitializeState m_state;
        private GlobalId? m_assetId;
        private TAsset m_asset;
        private TComponent m_component;

        protected DebugComponentInstance(IApplication application, string gameObjectName)
        {
            if (string.IsNullOrEmpty(gameObjectName)) throw new ArgumentException("Value cannot be null or empty.", nameof(gameObjectName));

            Application = application ?? throw new ArgumentNullException(nameof(application));
            GameObjectName = gameObjectName;

            AssetModule = Application.GetModule<IAssetModule>();
        }

        public void Load(GlobalId assetId)
        {
            if (!assetId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(assetId));

            m_state = m_state.Initialize();
            m_assetId = assetId;
            m_asset = AssetModule.Load<TAsset>(assetId);
            m_component = OnCreateComponent(m_asset);
        }

        public async Task LoadAsync(GlobalId assetId)
        {
            if (!assetId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(assetId));

            m_state = m_state.Initialize();
            m_assetId = assetId;
            m_asset = await AssetModule.LoadAsync<TAsset>(assetId);
            m_component = OnCreateComponent(m_asset);
        }

        public void Unload()
        {
            m_state = m_state.Uninitialize();

            OnDestroyComponent(m_component);

            AssetModule.Unload(AssetId, Asset);

            m_assetId = default;
            m_component = default;
            m_asset = default;
        }

        public async Task UnloadAsync()
        {
            m_state = m_state.Uninitialize();

            OnDestroyComponent(m_component);

            await AssetModule.UnloadAsync(AssetId, Asset);

            m_assetId = default;
            m_component = default;
            m_asset = default;
        }

        protected virtual TComponent OnCreateComponent(TAsset asset)
        {
            return new GameObject(GameObjectName).AddComponent<TComponent>();
        }

        protected virtual void OnDestroyComponent(TComponent component)
        {
            Object.Destroy(component.gameObject);
        }
    }
}
