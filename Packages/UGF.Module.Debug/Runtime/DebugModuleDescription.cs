using System;
using UGF.Application.Runtime;
using UGF.EditorTools.Runtime.Ids;

namespace UGF.Module.Debug.Runtime
{
    public class DebugModuleDescription : ApplicationModuleDescription
    {
        public bool DebugComponentLoadOnInitialize { get; }
        public bool DebugComponentLoadOnInitializeAsync { get; }
        public string DebugUIGameObjectName { get; }
        public string DebugGLGameObjectName { get; }
        public GlobalId DebugUIProviderId { get { return m_debugUIProviderId ?? throw new ArgumentException("Value not specified."); } }
        public bool HasDebugUIProviderId { get { return m_debugUIProviderId.HasValue; } }
        public GlobalId DebugGLProviderId { get { return m_debugGLProviderId ?? throw new ArgumentException("Value not specified."); } }
        public bool HasDebugGLProviderId { get { return m_debugGLProviderId.HasValue; } }

        private readonly GlobalId? m_debugUIProviderId;
        private readonly GlobalId? m_debugGLProviderId;

        public DebugModuleDescription(
            bool debugComponentLoadOnInitialize,
            bool debugComponentLoadOnInitializeAsync,
            string debugUIGameObjectName,
            string debugGLGameObjectName,
            GlobalId? debugUIProviderId,
            GlobalId? debugGLProviderId)
        {
            if (string.IsNullOrEmpty(debugUIGameObjectName)) throw new ArgumentException("Value cannot be null or empty.", nameof(debugUIGameObjectName));
            if (string.IsNullOrEmpty(debugGLGameObjectName)) throw new ArgumentException("Value cannot be null or empty.", nameof(debugGLGameObjectName));
            if (debugUIProviderId.HasValue && !debugUIProviderId.Value.IsValid()) throw new ArgumentException("Value should be valid.", nameof(debugUIProviderId));
            if (debugGLProviderId.HasValue && !debugGLProviderId.Value.IsValid()) throw new ArgumentException("Value should be valid.", nameof(debugGLProviderId));

            DebugComponentLoadOnInitialize = debugComponentLoadOnInitialize;
            DebugComponentLoadOnInitializeAsync = debugComponentLoadOnInitializeAsync;
            DebugUIGameObjectName = debugUIGameObjectName;
            DebugGLGameObjectName = debugGLGameObjectName;

            m_debugUIProviderId = debugUIProviderId;
            m_debugGLProviderId = debugGLProviderId;
        }
    }
}
