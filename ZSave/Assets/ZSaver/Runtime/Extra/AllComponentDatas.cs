// string longScript = "";
//
// foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
// {
// foreach (var type in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Component)) && !t.IsSubclassOf(typeof(MonoBehaviour))))
// {
//     longScript +=
//         "[System.Serializable]\npublic struct " + type.Name + "ZSaver {\n";
//
//
//     foreach (var propertyInfo in type
//         .GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance |
//                        BindingFlags.GetProperty).Where(c =>
//             c.GetCustomAttribute<ObsoleteAttribute>() == null))
//     {
//         longScript +=
//             $"    public {propertyInfo.PropertyType.ToString().Replace('+','.')} " + propertyInfo.Name + ";\n";
//     }
//
//     longScript += "}\n";
//
// }
// }
//         
// FileStream fs = new FileStream(Application.dataPath + "/ZSaver/Runtime/Extra/AllComponentDatas.cs", FileMode.Create);
// StreamWriter sw = new StreamWriter(fs);
//         
// sw.Write(longScript);
// sw.Close();

[System.Serializable]
public struct NavMeshAgentZSaver {
    public UnityEngine.Vector3 destination;
    public System.Single stoppingDistance;
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 nextPosition;
    public UnityEngine.Vector3 steeringTarget;
    public UnityEngine.Vector3 desiredVelocity;
    public System.Single remainingDistance;
    public System.Single baseOffset;
    public System.Boolean isOnOffMeshLink;
    public UnityEngine.AI.OffMeshLinkData currentOffMeshLinkData;
    public UnityEngine.AI.OffMeshLinkData nextOffMeshLinkData;
    public System.Boolean autoTraverseOffMeshLink;
    public System.Boolean autoBraking;
    public System.Boolean autoRepath;
    public System.Boolean hasPath;
    public System.Boolean pathPending;
    public System.Boolean isPathStale;
    public UnityEngine.AI.NavMeshPathStatus pathStatus;
    public UnityEngine.Vector3 pathEndPosition;
    public System.Boolean isStopped;
    public UnityEngine.AI.NavMeshPath path;
    public UnityEngine.Object navMeshOwner;
    public System.Int32 agentTypeID;
    public System.Int32 areaMask;
    public System.Single speed;
    public System.Single angularSpeed;
    public System.Single acceleration;
    public System.Boolean updatePosition;
    public System.Boolean updateRotation;
    public System.Boolean updateUpAxis;
    public System.Single radius;
    public System.Single height;
    public UnityEngine.AI.ObstacleAvoidanceType obstacleAvoidanceType;
    public System.Int32 avoidancePriority;
    public System.Boolean isOnNavMesh;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct NavMeshObstacleZSaver {
    public System.Single height;
    public System.Single radius;
    public UnityEngine.Vector3 velocity;
    public System.Boolean carving;
    public System.Boolean carveOnlyStationary;
    public System.Single carvingMoveThreshold;
    public System.Single carvingTimeToStationary;
    public UnityEngine.AI.NavMeshObstacleShape shape;
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct OffMeshLinkZSaver {
    public System.Boolean activated;
    public System.Boolean occupied;
    public System.Single costOverride;
    public System.Boolean biDirectional;
    public System.Int32 area;
    public System.Boolean autoUpdatePositions;
    public UnityEngine.Transform startTransform;
    public UnityEngine.Transform endTransform;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AnimatorZSaver {
    public System.Boolean isOptimizable;
    public System.Boolean isHuman;
    public System.Boolean hasRootMotion;
    public System.Single humanScale;
    public System.Boolean isInitialized;
    public UnityEngine.Vector3 deltaPosition;
    public UnityEngine.Quaternion deltaRotation;
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 angularVelocity;
    public UnityEngine.Vector3 rootPosition;
    public UnityEngine.Quaternion rootRotation;
    public System.Boolean applyRootMotion;
    public UnityEngine.AnimatorUpdateMode updateMode;
    public System.Boolean hasTransformHierarchy;
    public System.Single gravityWeight;
    public UnityEngine.Vector3 bodyPosition;
    public UnityEngine.Quaternion bodyRotation;
    public System.Boolean stabilizeFeet;
    public System.Int32 layerCount;
    public UnityEngine.AnimatorControllerParameter[] parameters;
    public System.Int32 parameterCount;
    public System.Single feetPivotActive;
    public System.Single pivotWeight;
    public UnityEngine.Vector3 pivotPosition;
    public System.Boolean isMatchingTarget;
    public System.Single speed;
    public UnityEngine.Vector3 targetPosition;
    public UnityEngine.Quaternion targetRotation;
    public UnityEngine.AnimatorCullingMode cullingMode;
    public System.Single playbackTime;
    public System.Single recorderStartTime;
    public System.Single recorderStopTime;
    public UnityEngine.AnimatorRecorderMode recorderMode;
    public UnityEngine.RuntimeAnimatorController runtimeAnimatorController;
    public System.Boolean hasBoundPlayables;
    public UnityEngine.Avatar avatar;
    public UnityEngine.Playables.PlayableGraph playableGraph;
    public System.Boolean layersAffectMassCenter;
    public System.Single leftFeetBottomHeight;
    public System.Single rightFeetBottomHeight;
    public System.Boolean logWarnings;
    public System.Boolean fireEvents;
    public System.Boolean keepAnimatorControllerStateOnDisable;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AnimationZSaver {
    public UnityEngine.AnimationClip clip;
    public System.Boolean playAutomatically;
    public UnityEngine.WrapMode wrapMode;
    public System.Boolean isPlaying;
    public UnityEngine.AnimationState Item;
    public System.Boolean animatePhysics;
    public UnityEngine.AnimationCullingType cullingType;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AimConstraintZSaver {
    public System.Single weight;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Animations.Axis rotationAxis;
    public UnityEngine.Vector3 aimVector;
    public UnityEngine.Vector3 upVector;
    public UnityEngine.Vector3 worldUpVector;
    public UnityEngine.Transform worldUpObject;
    public UnityEngine.Animations.AimConstraint.WorldUpType worldUpType;
    public System.Int32 sourceCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PositionConstraintZSaver {
    public System.Single weight;
    public UnityEngine.Vector3 translationAtRest;
    public UnityEngine.Vector3 translationOffset;
    public UnityEngine.Animations.Axis translationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Int32 sourceCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct RotationConstraintZSaver {
    public System.Single weight;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Animations.Axis rotationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Int32 sourceCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ScaleConstraintZSaver {
    public System.Single weight;
    public UnityEngine.Vector3 scaleAtRest;
    public UnityEngine.Vector3 scaleOffset;
    public UnityEngine.Animations.Axis scalingAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Int32 sourceCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LookAtConstraintZSaver {
    public System.Single weight;
    public System.Single roll;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Transform worldUpObject;
    public System.Boolean useUpObject;
    public System.Int32 sourceCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ParentConstraintZSaver {
    public System.Single weight;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Int32 sourceCount;
    public UnityEngine.Vector3 translationAtRest;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3[] translationOffsets;
    public UnityEngine.Vector3[] rotationOffsets;
    public UnityEngine.Animations.Axis translationAxis;
    public UnityEngine.Animations.Axis rotationAxis;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioSourceZSaver {
    public System.Single volume;
    public System.Single pitch;
    public System.Single time;
    public System.Int32 timeSamples;
    public UnityEngine.AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup outputAudioMixerGroup;
    public System.Boolean isPlaying;
    public System.Boolean isVirtual;
    public System.Boolean loop;
    public System.Boolean ignoreListenerVolume;
    public System.Boolean playOnAwake;
    public System.Boolean ignoreListenerPause;
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Single panStereo;
    public System.Single spatialBlend;
    public System.Boolean spatialize;
    public System.Boolean spatializePostEffects;
    public System.Single reverbZoneMix;
    public System.Boolean bypassEffects;
    public System.Boolean bypassListenerEffects;
    public System.Boolean bypassReverbZones;
    public System.Single dopplerLevel;
    public System.Single spread;
    public System.Int32 priority;
    public System.Boolean mute;
    public System.Single minDistance;
    public System.Single maxDistance;
    public UnityEngine.AudioRolloffMode rolloffMode;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioLowPassFilterZSaver {
    public UnityEngine.AnimationCurve customCutoffCurve;
    public System.Single cutoffFrequency;
    public System.Single lowpassResonanceQ;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioHighPassFilterZSaver {
    public System.Single cutoffFrequency;
    public System.Single highpassResonanceQ;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioReverbFilterZSaver {
    public UnityEngine.AudioReverbPreset reverbPreset;
    public System.Single dryLevel;
    public System.Single room;
    public System.Single roomHF;
    public System.Single decayTime;
    public System.Single decayHFRatio;
    public System.Single reflectionsLevel;
    public System.Single reflectionsDelay;
    public System.Single reverbLevel;
    public System.Single reverbDelay;
    public System.Single diffusion;
    public System.Single density;
    public System.Single hfReference;
    public System.Single roomLF;
    public System.Single lfReference;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioBehaviourZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioListenerZSaver {
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioReverbZoneZSaver {
    public System.Single minDistance;
    public System.Single maxDistance;
    public UnityEngine.AudioReverbPreset reverbPreset;
    public System.Int32 room;
    public System.Int32 roomHF;
    public System.Int32 roomLF;
    public System.Single decayTime;
    public System.Single decayHFRatio;
    public System.Int32 reflections;
    public System.Single reflectionsDelay;
    public System.Int32 reverb;
    public System.Single reverbDelay;
    public System.Single HFReference;
    public System.Single LFReference;
    public System.Single diffusion;
    public System.Single density;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioDistortionFilterZSaver {
    public System.Single distortionLevel;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioEchoFilterZSaver {
    public System.Single delay;
    public System.Single decayRatio;
    public System.Single dryMix;
    public System.Single wetMix;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AudioChorusFilterZSaver {
    public System.Single dryMix;
    public System.Single wetMix1;
    public System.Single wetMix2;
    public System.Single wetMix3;
    public System.Single delay;
    public System.Single rate;
    public System.Single depth;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ClothZSaver {
    public UnityEngine.Vector3[] vertices;
    public UnityEngine.Vector3[] normals;
    public UnityEngine.ClothSkinningCoefficient[] coefficients;
    public UnityEngine.CapsuleCollider[] capsuleColliders;
    public UnityEngine.ClothSphereColliderPair[] sphereColliders;
    public System.Single sleepThreshold;
    public System.Single bendingStiffness;
    public System.Single stretchingStiffness;
    public System.Single damping;
    public UnityEngine.Vector3 externalAcceleration;
    public UnityEngine.Vector3 randomAcceleration;
    public System.Boolean useGravity;
    public System.Boolean enabled;
    public System.Single friction;
    public System.Single collisionMassScale;
    public System.Boolean enableContinuousCollision;
    public System.Single useVirtualParticles;
    public System.Single worldVelocityScale;
    public System.Single worldAccelerationScale;
    public System.Single clothSolverFrequency;
    public System.Boolean useTethers;
    public System.Single stiffnessFrequency;
    public System.Single selfCollisionDistance;
    public System.Single selfCollisionStiffness;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CameraZSaver {
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single fieldOfView;
    public UnityEngine.RenderingPath renderingPath;
    public UnityEngine.RenderingPath actualRenderingPath;
    public System.Boolean allowHDR;
    public System.Boolean allowMSAA;
    public System.Boolean allowDynamicResolution;
    public System.Boolean forceIntoRenderTexture;
    public System.Single orthographicSize;
    public System.Boolean orthographic;
    public UnityEngine.Rendering.OpaqueSortMode opaqueSortMode;
    public UnityEngine.TransparencySortMode transparencySortMode;
    public UnityEngine.Vector3 transparencySortAxis;
    public System.Single depth;
    public System.Single aspect;
    public UnityEngine.Vector3 velocity;
    public System.Int32 cullingMask;
    public System.Int32 eventMask;
    public System.Boolean layerCullSpherical;
    public UnityEngine.CameraType cameraType;
    public System.UInt64 overrideSceneCullingMask;
    public System.Single[] layerCullDistances;
    public System.Boolean useOcclusionCulling;
    public UnityEngine.Matrix4x4 cullingMatrix;
    public UnityEngine.Color backgroundColor;
    public UnityEngine.CameraClearFlags clearFlags;
    public UnityEngine.DepthTextureMode depthTextureMode;
    public System.Boolean clearStencilAfterLightingPass;
    public System.Boolean usePhysicalProperties;
    public UnityEngine.Vector2 sensorSize;
    public UnityEngine.Vector2 lensShift;
    public System.Single focalLength;
    public UnityEngine.Camera.GateFitMode gateFit;
    public UnityEngine.Rect rect;
    public UnityEngine.Rect pixelRect;
    public System.Int32 pixelWidth;
    public System.Int32 pixelHeight;
    public System.Int32 scaledPixelWidth;
    public System.Int32 scaledPixelHeight;
    public UnityEngine.RenderTexture targetTexture;
    public UnityEngine.RenderTexture activeTexture;
    public System.Int32 targetDisplay;
    public UnityEngine.Matrix4x4 cameraToWorldMatrix;
    public UnityEngine.Matrix4x4 worldToCameraMatrix;
    public UnityEngine.Matrix4x4 projectionMatrix;
    public UnityEngine.Matrix4x4 nonJitteredProjectionMatrix;
    public System.Boolean useJitteredProjectionMatrixForTransparentRendering;
    public UnityEngine.Matrix4x4 previousViewProjectionMatrix;
    public UnityEngine.SceneManagement.Scene scene;
    public System.Boolean stereoEnabled;
    public System.Single stereoSeparation;
    public System.Single stereoConvergence;
    public System.Boolean areVRStereoViewMatricesWithinSingleCullTolerance;
    public UnityEngine.StereoTargetEyeMask stereoTargetEye;
    public UnityEngine.Camera.MonoOrStereoscopicEye stereoActiveEye;
    public System.Int32 commandBufferCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct FlareLayerZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ReflectionProbeZSaver {
    public UnityEngine.Vector3 size;
    public UnityEngine.Vector3 center;
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single intensity;
    public UnityEngine.Bounds bounds;
    public System.Boolean hdr;
    public System.Boolean renderDynamicObjects;
    public System.Single shadowDistance;
    public System.Int32 resolution;
    public System.Int32 cullingMask;
    public UnityEngine.Rendering.ReflectionProbeClearFlags clearFlags;
    public UnityEngine.Color backgroundColor;
    public System.Single blendDistance;
    public System.Boolean boxProjection;
    public UnityEngine.Rendering.ReflectionProbeMode mode;
    public System.Int32 importance;
    public UnityEngine.Rendering.ReflectionProbeRefreshMode refreshMode;
    public UnityEngine.Rendering.ReflectionProbeTimeSlicingMode timeSlicingMode;
    public UnityEngine.Texture bakedTexture;
    public UnityEngine.Texture customBakedTexture;
    public UnityEngine.RenderTexture realtimeTexture;
    public UnityEngine.Texture texture;
    public UnityEngine.Vector4 textureHDRDecodeValues;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct BillboardRendererZSaver {
    public UnityEngine.BillboardAsset billboard;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct RendererZSaver {
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ProjectorZSaver {
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single fieldOfView;
    public System.Single aspectRatio;
    public System.Boolean orthographic;
    public System.Single orthographicSize;
    public System.Int32 ignoreLayers;
    public UnityEngine.Material material;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TrailRendererZSaver {
    public System.Single time;
    public System.Single startWidth;
    public System.Single endWidth;
    public System.Single widthMultiplier;
    public System.Boolean autodestruct;
    public System.Boolean emitting;
    public System.Int32 numCornerVertices;
    public System.Int32 numCapVertices;
    public System.Single minVertexDistance;
    public UnityEngine.Color startColor;
    public UnityEngine.Color endColor;
    public System.Int32 positionCount;
    public System.Single shadowBias;
    public System.Boolean generateLightingData;
    public UnityEngine.LineTextureMode textureMode;
    public UnityEngine.LineAlignment alignment;
    public UnityEngine.AnimationCurve widthCurve;
    public UnityEngine.Gradient colorGradient;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LineRendererZSaver {
    public System.Single startWidth;
    public System.Single endWidth;
    public System.Single widthMultiplier;
    public System.Int32 numCornerVertices;
    public System.Int32 numCapVertices;
    public System.Boolean useWorldSpace;
    public System.Boolean loop;
    public UnityEngine.Color startColor;
    public UnityEngine.Color endColor;
    public System.Int32 positionCount;
    public System.Single shadowBias;
    public System.Boolean generateLightingData;
    public UnityEngine.LineTextureMode textureMode;
    public UnityEngine.LineAlignment alignment;
    public UnityEngine.AnimationCurve widthCurve;
    public UnityEngine.Gradient colorGradient;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct OcclusionPortalZSaver {
    public System.Boolean open;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct OcclusionAreaZSaver {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LensFlareZSaver {
    public System.Single brightness;
    public System.Single fadeSpeed;
    public UnityEngine.Color color;
    public UnityEngine.Flare flare;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LightZSaver {
    public UnityEngine.LightType type;
    public UnityEngine.LightShape shape;
    public System.Single spotAngle;
    public System.Single innerSpotAngle;
    public UnityEngine.Color color;
    public System.Single colorTemperature;
    public System.Boolean useColorTemperature;
    public System.Single intensity;
    public System.Single bounceIntensity;
    public System.Boolean useBoundingSphereOverride;
    public UnityEngine.Vector4 boundingSphereOverride;
    public System.Boolean useViewFrustumForShadowCasterCull;
    public System.Int32 shadowCustomResolution;
    public System.Single shadowBias;
    public System.Single shadowNormalBias;
    public System.Single shadowNearPlane;
    public System.Boolean useShadowMatrixOverride;
    public UnityEngine.Matrix4x4 shadowMatrixOverride;
    public System.Single range;
    public UnityEngine.Flare flare;
    public UnityEngine.LightBakingOutput bakingOutput;
    public System.Int32 cullingMask;
    public System.Int32 renderingLayerMask;
    public UnityEngine.LightShadowCasterMode lightShadowCasterMode;
    public System.Single shadowRadius;
    public System.Single shadowAngle;
    public UnityEngine.LightShadows shadows;
    public System.Single shadowStrength;
    public UnityEngine.Rendering.LightShadowResolution shadowResolution;
    public System.Single[] layerShadowCullDistances;
    public System.Single cookieSize;
    public UnityEngine.Texture cookie;
    public UnityEngine.LightRenderMode renderMode;
    public UnityEngine.Vector2 areaSize;
    public UnityEngine.LightmapBakeType lightmapBakeType;
    public System.Int32 commandBufferCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SkyboxZSaver {
    public UnityEngine.Material material;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct MeshFilterZSaver {
    public UnityEngine.Mesh sharedMesh;
    public UnityEngine.Mesh mesh;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct HaloZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LightProbeProxyVolumeZSaver {
    public UnityEngine.Bounds boundsGlobal;
    public UnityEngine.Vector3 sizeCustom;
    public UnityEngine.Vector3 originCustom;
    public System.Single probeDensity;
    public System.Int32 gridResolutionX;
    public System.Int32 gridResolutionY;
    public System.Int32 gridResolutionZ;
    public UnityEngine.LightProbeProxyVolume.BoundingBoxMode boundingBoxMode;
    public UnityEngine.LightProbeProxyVolume.ResolutionMode resolutionMode;
    public UnityEngine.LightProbeProxyVolume.ProbePositionMode probePositionMode;
    public UnityEngine.LightProbeProxyVolume.RefreshMode refreshMode;
    public UnityEngine.LightProbeProxyVolume.QualityMode qualityMode;
    public UnityEngine.LightProbeProxyVolume.DataFormat dataFormat;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SkinnedMeshRendererZSaver {
    public UnityEngine.SkinQuality quality;
    public System.Boolean updateWhenOffscreen;
    public System.Boolean forceMatrixRecalculationPerRender;
    public UnityEngine.Transform rootBone;
    public UnityEngine.Transform[] bones;
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean skinnedMotionVectors;
    public UnityEngine.Bounds localBounds;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct MeshRendererZSaver {
    public UnityEngine.Mesh additionalVertexStreams;
    public UnityEngine.Mesh enlightenVertexStream;
    public System.Int32 subMeshStartIndex;
    public System.Single scaleInLightmap;
    public UnityEngine.ReceiveGI receiveGI;
    public System.Boolean stitchLightmapSeams;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LightProbeGroupZSaver {
    public UnityEngine.Vector3[] probePositions;
    public System.Boolean dering;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct LODGroupZSaver {
    public UnityEngine.Vector3 localReferencePoint;
    public System.Single size;
    public System.Int32 lodCount;
    public UnityEngine.LODFadeMode fadeMode;
    public System.Boolean animateCrossFading;
    public System.Boolean enabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct NetworkViewZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct BehaviourZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct MonoBehaviourZSaver {
    public System.Boolean useGUILayout;
    public System.Boolean runInEditMode;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct RectTransformZSaver {
    public UnityEngine.Rect rect;
    public UnityEngine.Vector2 anchorMin;
    public UnityEngine.Vector2 anchorMax;
    public UnityEngine.Vector2 anchoredPosition;
    public UnityEngine.Vector2 sizeDelta;
    public UnityEngine.Vector2 pivot;
    public UnityEngine.Vector3 anchoredPosition3D;
    public UnityEngine.Vector2 offsetMin;
    public UnityEngine.Vector2 offsetMax;
    public UnityEngine.Vector3 position;
    public UnityEngine.Vector3 localPosition;
    public UnityEngine.Vector3 eulerAngles;
    public UnityEngine.Vector3 localEulerAngles;
    public UnityEngine.Vector3 right;
    public UnityEngine.Vector3 up;
    public UnityEngine.Vector3 forward;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.Quaternion localRotation;
    public UnityEngine.Vector3 localScale;
    public UnityEngine.Transform parent;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.Transform root;
    public System.Int32 childCount;
    public UnityEngine.Vector3 lossyScale;
    public System.Boolean hasChanged;
    public System.Int32 hierarchyCapacity;
    public System.Int32 hierarchyCount;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TransformZSaver {
    public UnityEngine.Vector3 position;
    public UnityEngine.Vector3 localPosition;
    public UnityEngine.Vector3 eulerAngles;
    public UnityEngine.Vector3 localEulerAngles;
    public UnityEngine.Vector3 right;
    public UnityEngine.Vector3 up;
    public UnityEngine.Vector3 forward;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.Quaternion localRotation;
    public UnityEngine.Vector3 localScale;
    public UnityEngine.Transform parent;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.Transform root;
    public System.Int32 childCount;
    public UnityEngine.Vector3 lossyScale;
    public System.Boolean hasChanged;
    public System.Int32 hierarchyCapacity;
    public System.Int32 hierarchyCount;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SpriteRendererZSaver {
    public UnityEngine.Sprite sprite;
    public UnityEngine.SpriteDrawMode drawMode;
    public UnityEngine.Vector2 size;
    public System.Single adaptiveModeThreshold;
    public UnityEngine.SpriteTileMode tileMode;
    public UnityEngine.Color color;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public System.Boolean flipX;
    public System.Boolean flipY;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SortingGroupZSaver {
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PlayableDirectorZSaver {
    public UnityEngine.Playables.PlayState state;
    public UnityEngine.Playables.DirectorWrapMode extrapolationMode;
    public UnityEngine.Playables.PlayableAsset playableAsset;
    public UnityEngine.Playables.PlayableGraph playableGraph;
    public System.Boolean playOnAwake;
    public UnityEngine.Playables.DirectorUpdateMode timeUpdateMode;
    public System.Double time;
    public System.Double initialTime;
    public System.Double duration;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct GridZSaver {
    public UnityEngine.Vector3 cellSize;
    public UnityEngine.Vector3 cellGap;
    public UnityEngine.GridLayout.CellLayout cellLayout;
    public UnityEngine.GridLayout.CellSwizzle cellSwizzle;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct GridLayoutZSaver {
    public UnityEngine.Vector3 cellSize;
    public UnityEngine.Vector3 cellGap;
    public UnityEngine.GridLayout.CellLayout cellLayout;
    public UnityEngine.GridLayout.CellSwizzle cellSwizzle;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ParticleSystemZSaver {
    public System.Boolean isPlaying;
    public System.Boolean isEmitting;
    public System.Boolean isStopped;
    public System.Boolean isPaused;
    public System.Int32 particleCount;
    public System.Single time;
    public System.UInt32 randomSeed;
    public System.Boolean useAutoRandomSeed;
    public System.Boolean proceduralSimulationSupported;
    public UnityEngine.ParticleSystem.MainModule main;
    public UnityEngine.ParticleSystem.EmissionModule emission;
    public UnityEngine.ParticleSystem.ShapeModule shape;
    public UnityEngine.ParticleSystem.VelocityOverLifetimeModule velocityOverLifetime;
    public UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime;
    public UnityEngine.ParticleSystem.InheritVelocityModule inheritVelocity;
    public UnityEngine.ParticleSystem.LifetimeByEmitterSpeedModule lifetimeByEmitterSpeed;
    public UnityEngine.ParticleSystem.ForceOverLifetimeModule forceOverLifetime;
    public UnityEngine.ParticleSystem.ColorOverLifetimeModule colorOverLifetime;
    public UnityEngine.ParticleSystem.ColorBySpeedModule colorBySpeed;
    public UnityEngine.ParticleSystem.SizeOverLifetimeModule sizeOverLifetime;
    public UnityEngine.ParticleSystem.SizeBySpeedModule sizeBySpeed;
    public UnityEngine.ParticleSystem.RotationOverLifetimeModule rotationOverLifetime;
    public UnityEngine.ParticleSystem.RotationBySpeedModule rotationBySpeed;
    public UnityEngine.ParticleSystem.ExternalForcesModule externalForces;
    public UnityEngine.ParticleSystem.NoiseModule noise;
    public UnityEngine.ParticleSystem.CollisionModule collision;
    public UnityEngine.ParticleSystem.TriggerModule trigger;
    public UnityEngine.ParticleSystem.SubEmittersModule subEmitters;
    public UnityEngine.ParticleSystem.TextureSheetAnimationModule textureSheetAnimation;
    public UnityEngine.ParticleSystem.LightsModule lights;
    public UnityEngine.ParticleSystem.TrailModule trails;
    public UnityEngine.ParticleSystem.CustomDataModule customData;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ParticleSystemRendererZSaver {
    public UnityEngine.ParticleSystemRenderSpace alignment;
    public UnityEngine.ParticleSystemRenderMode renderMode;
    public UnityEngine.ParticleSystemSortMode sortMode;
    public System.Single lengthScale;
    public System.Single velocityScale;
    public System.Single cameraVelocityScale;
    public System.Single normalDirection;
    public System.Single shadowBias;
    public System.Single sortingFudge;
    public System.Single minParticleSize;
    public System.Single maxParticleSize;
    public UnityEngine.Vector3 pivot;
    public UnityEngine.Vector3 flip;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public UnityEngine.Material trailMaterial;
    public System.Boolean enableGPUInstancing;
    public System.Boolean allowRoll;
    public System.Boolean freeformStretching;
    public System.Boolean rotateWithStretchDirection;
    public UnityEngine.Mesh mesh;
    public System.Int32 meshCount;
    public System.Int32 activeVertexStreamsCount;
    public System.Boolean supportsMeshInstancing;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ParticleSystemForceFieldZSaver {
    public UnityEngine.ParticleSystemForceFieldShape shape;
    public System.Single startRange;
    public System.Single endRange;
    public System.Single length;
    public System.Single gravityFocus;
    public UnityEngine.Vector2 rotationRandomness;
    public System.Boolean multiplyDragByParticleSize;
    public System.Boolean multiplyDragByParticleVelocity;
    public UnityEngine.Texture3D vectorField;
    public UnityEngine.ParticleSystem.MinMaxCurve directionX;
    public UnityEngine.ParticleSystem.MinMaxCurve directionY;
    public UnityEngine.ParticleSystem.MinMaxCurve directionZ;
    public UnityEngine.ParticleSystem.MinMaxCurve gravity;
    public UnityEngine.ParticleSystem.MinMaxCurve rotationSpeed;
    public UnityEngine.ParticleSystem.MinMaxCurve rotationAttraction;
    public UnityEngine.ParticleSystem.MinMaxCurve drag;
    public UnityEngine.ParticleSystem.MinMaxCurve vectorFieldSpeed;
    public UnityEngine.ParticleSystem.MinMaxCurve vectorFieldAttraction;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct RigidbodyZSaver {
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 angularVelocity;
    public System.Single drag;
    public System.Single angularDrag;
    public System.Single mass;
    public System.Boolean useGravity;
    public System.Single maxDepenetrationVelocity;
    public System.Boolean isKinematic;
    public System.Boolean freezeRotation;
    public UnityEngine.RigidbodyConstraints constraints;
    public UnityEngine.CollisionDetectionMode collisionDetectionMode;
    public UnityEngine.Vector3 centerOfMass;
    public UnityEngine.Vector3 worldCenterOfMass;
    public UnityEngine.Quaternion inertiaTensorRotation;
    public UnityEngine.Vector3 inertiaTensor;
    public System.Boolean detectCollisions;
    public UnityEngine.Vector3 position;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.RigidbodyInterpolation interpolation;
    public System.Int32 solverIterations;
    public System.Single sleepThreshold;
    public System.Single maxAngularVelocity;
    public System.Int32 solverVelocityIterations;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ColliderZSaver {
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CharacterControllerZSaver {
    public UnityEngine.Vector3 velocity;
    public System.Boolean isGrounded;
    public UnityEngine.CollisionFlags collisionFlags;
    public System.Single radius;
    public System.Single height;
    public UnityEngine.Vector3 center;
    public System.Single slopeLimit;
    public System.Single stepOffset;
    public System.Single skinWidth;
    public System.Single minMoveDistance;
    public System.Boolean detectCollisions;
    public System.Boolean enableOverlapRecovery;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct MeshColliderZSaver {
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean convex;
    public UnityEngine.MeshColliderCookingOptions cookingOptions;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CapsuleColliderZSaver {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Single height;
    public System.Int32 direction;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct BoxColliderZSaver {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SphereColliderZSaver {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ConstantForceZSaver {
    public UnityEngine.Vector3 force;
    public UnityEngine.Vector3 relativeForce;
    public UnityEngine.Vector3 torque;
    public UnityEngine.Vector3 relativeTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct JointZSaver {
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct HingeJointZSaver {
    public UnityEngine.JointMotor motor;
    public UnityEngine.JointLimits limits;
    public UnityEngine.JointSpring spring;
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public System.Boolean useSpring;
    public System.Single velocity;
    public System.Single angle;
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SpringJointZSaver {
    public System.Single spring;
    public System.Single damper;
    public System.Single minDistance;
    public System.Single maxDistance;
    public System.Single tolerance;
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct FixedJointZSaver {
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CharacterJointZSaver {
    public UnityEngine.Vector3 swingAxis;
    public UnityEngine.SoftJointLimitSpring twistLimitSpring;
    public UnityEngine.SoftJointLimitSpring swingLimitSpring;
    public UnityEngine.SoftJointLimit lowTwistLimit;
    public UnityEngine.SoftJointLimit highTwistLimit;
    public UnityEngine.SoftJointLimit swing1Limit;
    public UnityEngine.SoftJointLimit swing2Limit;
    public System.Boolean enableProjection;
    public System.Single projectionDistance;
    public System.Single projectionAngle;
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ConfigurableJointZSaver {
    public UnityEngine.Vector3 secondaryAxis;
    public UnityEngine.ConfigurableJointMotion xMotion;
    public UnityEngine.ConfigurableJointMotion yMotion;
    public UnityEngine.ConfigurableJointMotion zMotion;
    public UnityEngine.ConfigurableJointMotion angularXMotion;
    public UnityEngine.ConfigurableJointMotion angularYMotion;
    public UnityEngine.ConfigurableJointMotion angularZMotion;
    public UnityEngine.SoftJointLimitSpring linearLimitSpring;
    public UnityEngine.SoftJointLimitSpring angularXLimitSpring;
    public UnityEngine.SoftJointLimitSpring angularYZLimitSpring;
    public UnityEngine.SoftJointLimit linearLimit;
    public UnityEngine.SoftJointLimit lowAngularXLimit;
    public UnityEngine.SoftJointLimit highAngularXLimit;
    public UnityEngine.SoftJointLimit angularYLimit;
    public UnityEngine.SoftJointLimit angularZLimit;
    public UnityEngine.Vector3 targetPosition;
    public UnityEngine.Vector3 targetVelocity;
    public UnityEngine.JointDrive xDrive;
    public UnityEngine.JointDrive yDrive;
    public UnityEngine.JointDrive zDrive;
    public UnityEngine.Quaternion targetRotation;
    public UnityEngine.Vector3 targetAngularVelocity;
    public UnityEngine.RotationDriveMode rotationDriveMode;
    public UnityEngine.JointDrive angularXDrive;
    public UnityEngine.JointDrive angularYZDrive;
    public UnityEngine.JointDrive slerpDrive;
    public UnityEngine.JointProjectionMode projectionMode;
    public System.Single projectionDistance;
    public System.Single projectionAngle;
    public System.Boolean configuredInWorldSpace;
    public System.Boolean swapBodies;
    public UnityEngine.Rigidbody connectedBody;
    public UnityEngine.ArticulationBody connectedArticulationBody;
    public UnityEngine.Vector3 axis;
    public UnityEngine.Vector3 anchor;
    public UnityEngine.Vector3 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enableCollision;
    public System.Boolean enablePreprocessing;
    public System.Single massScale;
    public System.Single connectedMassScale;
    public UnityEngine.Vector3 currentForce;
    public UnityEngine.Vector3 currentTorque;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ArticulationBodyZSaver {
    public UnityEngine.ArticulationJointType jointType;
    public UnityEngine.Vector3 anchorPosition;
    public UnityEngine.Vector3 parentAnchorPosition;
    public UnityEngine.Quaternion anchorRotation;
    public UnityEngine.Quaternion parentAnchorRotation;
    public System.Boolean isRoot;
    public UnityEngine.ArticulationDofLock linearLockX;
    public UnityEngine.ArticulationDofLock linearLockY;
    public UnityEngine.ArticulationDofLock linearLockZ;
    public UnityEngine.ArticulationDofLock swingYLock;
    public UnityEngine.ArticulationDofLock swingZLock;
    public UnityEngine.ArticulationDofLock twistLock;
    public UnityEngine.ArticulationDrive xDrive;
    public UnityEngine.ArticulationDrive yDrive;
    public UnityEngine.ArticulationDrive zDrive;
    public System.Boolean immovable;
    public System.Boolean useGravity;
    public System.Single linearDamping;
    public System.Single angularDamping;
    public System.Single jointFriction;
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 angularVelocity;
    public System.Single mass;
    public UnityEngine.Vector3 centerOfMass;
    public UnityEngine.Vector3 worldCenterOfMass;
    public UnityEngine.Vector3 inertiaTensor;
    public UnityEngine.Quaternion inertiaTensorRotation;
    public System.Single sleepThreshold;
    public System.Int32 solverIterations;
    public System.Int32 solverVelocityIterations;
    public System.Single maxAngularVelocity;
    public System.Single maxLinearVelocity;
    public System.Single maxJointVelocity;
    public System.Single maxDepenetrationVelocity;
    public UnityEngine.ArticulationReducedSpace jointPosition;
    public UnityEngine.ArticulationReducedSpace jointVelocity;
    public UnityEngine.ArticulationReducedSpace jointAcceleration;
    public UnityEngine.ArticulationReducedSpace jointForce;
    public System.Int32 dofCount;
    public System.Int32 index;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct Rigidbody2DZSaver {
    public UnityEngine.Vector2 position;
    public System.Single rotation;
    public UnityEngine.Vector2 velocity;
    public System.Single angularVelocity;
    public System.Boolean useAutoMass;
    public System.Single mass;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public UnityEngine.Vector2 centerOfMass;
    public UnityEngine.Vector2 worldCenterOfMass;
    public System.Single inertia;
    public System.Single drag;
    public System.Single angularDrag;
    public System.Single gravityScale;
    public UnityEngine.RigidbodyType2D bodyType;
    public System.Boolean useFullKinematicContacts;
    public System.Boolean isKinematic;
    public System.Boolean freezeRotation;
    public UnityEngine.RigidbodyConstraints2D constraints;
    public System.Boolean simulated;
    public UnityEngine.RigidbodyInterpolation2D interpolation;
    public UnityEngine.RigidbodySleepMode2D sleepMode;
    public UnityEngine.CollisionDetectionMode2D collisionDetectionMode;
    public System.Int32 attachedColliderCount;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct Collider2DZSaver {
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CircleCollider2DZSaver {
    public System.Single radius;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CapsuleCollider2DZSaver {
    public UnityEngine.Vector2 size;
    public UnityEngine.CapsuleDirection2D direction;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct EdgeCollider2DZSaver {
    public System.Single edgeRadius;
    public System.Int32 edgeCount;
    public System.Int32 pointCount;
    public UnityEngine.Vector2[] points;
    public System.Boolean useAdjacentStartPoint;
    public System.Boolean useAdjacentEndPoint;
    public UnityEngine.Vector2 adjacentStartPoint;
    public UnityEngine.Vector2 adjacentEndPoint;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct BoxCollider2DZSaver {
    public UnityEngine.Vector2 size;
    public System.Single edgeRadius;
    public System.Boolean autoTiling;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PolygonCollider2DZSaver {
    public System.Boolean autoTiling;
    public UnityEngine.Vector2[] points;
    public System.Int32 pathCount;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CompositeCollider2DZSaver {
    public UnityEngine.CompositeCollider2D.GeometryType geometryType;
    public UnityEngine.CompositeCollider2D.GenerationType generationType;
    public System.Single vertexDistance;
    public System.Single edgeRadius;
    public System.Single offsetDistance;
    public System.Int32 pathCount;
    public System.Int32 pointCount;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct Joint2DZSaver {
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AnchoredJoint2DZSaver {
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SpringJoint2DZSaver {
    public System.Boolean autoConfigureDistance;
    public System.Single distance;
    public System.Single dampingRatio;
    public System.Single frequency;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct DistanceJoint2DZSaver {
    public System.Boolean autoConfigureDistance;
    public System.Single distance;
    public System.Boolean maxDistanceOnly;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct FrictionJoint2DZSaver {
    public System.Single maxForce;
    public System.Single maxTorque;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct HingeJoint2DZSaver {
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public UnityEngine.JointMotor2D motor;
    public UnityEngine.JointAngleLimits2D limits;
    public UnityEngine.JointLimitState2D limitState;
    public System.Single referenceAngle;
    public System.Single jointAngle;
    public System.Single jointSpeed;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct RelativeJoint2DZSaver {
    public System.Single maxForce;
    public System.Single maxTorque;
    public System.Single correctionScale;
    public System.Boolean autoConfigureOffset;
    public UnityEngine.Vector2 linearOffset;
    public System.Single angularOffset;
    public UnityEngine.Vector2 target;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SliderJoint2DZSaver {
    public System.Boolean autoConfigureAngle;
    public System.Single angle;
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public UnityEngine.JointMotor2D motor;
    public UnityEngine.JointTranslationLimits2D limits;
    public UnityEngine.JointLimitState2D limitState;
    public System.Single referenceAngle;
    public System.Single jointTranslation;
    public System.Single jointSpeed;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TargetJoint2DZSaver {
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 target;
    public System.Boolean autoConfigureTarget;
    public System.Single maxForce;
    public System.Single dampingRatio;
    public System.Single frequency;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct FixedJoint2DZSaver {
    public System.Single dampingRatio;
    public System.Single frequency;
    public System.Single referenceAngle;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct WheelJoint2DZSaver {
    public UnityEngine.JointSuspension2D suspension;
    public System.Boolean useMotor;
    public UnityEngine.JointMotor2D motor;
    public System.Single jointTranslation;
    public System.Single jointLinearSpeed;
    public System.Single jointSpeed;
    public System.Single jointAngle;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public UnityEngine.Vector2 reactionForce;
    public System.Single reactionTorque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct Effector2DZSaver {
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct AreaEffector2DZSaver {
    public System.Single forceAngle;
    public System.Boolean useGlobalAngle;
    public System.Single forceMagnitude;
    public System.Single forceVariation;
    public System.Single drag;
    public System.Single angularDrag;
    public UnityEngine.EffectorSelection2D forceTarget;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct BuoyancyEffector2DZSaver {
    public System.Single surfaceLevel;
    public System.Single density;
    public System.Single linearDrag;
    public System.Single angularDrag;
    public System.Single flowAngle;
    public System.Single flowMagnitude;
    public System.Single flowVariation;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PointEffector2DZSaver {
    public System.Single forceMagnitude;
    public System.Single forceVariation;
    public System.Single distanceScale;
    public System.Single drag;
    public System.Single angularDrag;
    public UnityEngine.EffectorSelection2D forceSource;
    public UnityEngine.EffectorSelection2D forceTarget;
    public UnityEngine.EffectorForceMode2D forceMode;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PlatformEffector2DZSaver {
    public System.Boolean useOneWay;
    public System.Boolean useOneWayGrouping;
    public System.Boolean useSideFriction;
    public System.Boolean useSideBounce;
    public System.Single surfaceArc;
    public System.Single sideArc;
    public System.Single rotationalOffset;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SurfaceEffector2DZSaver {
    public System.Single speed;
    public System.Single speedVariation;
    public System.Single forceScale;
    public System.Boolean useContactForce;
    public System.Boolean useFriction;
    public System.Boolean useBounce;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct PhysicsUpdateBehaviour2DZSaver {
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct ConstantForce2DZSaver {
    public UnityEngine.Vector2 force;
    public UnityEngine.Vector2 relativeForce;
    public System.Single torque;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SpriteMaskZSaver {
    public System.Int32 frontSortingLayerID;
    public System.Int32 frontSortingOrder;
    public System.Int32 backSortingLayerID;
    public System.Int32 backSortingOrder;
    public System.Single alphaCutoff;
    public UnityEngine.Sprite sprite;
    public System.Boolean isCustomRangeActive;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct SpriteShapeRendererZSaver {
    public UnityEngine.Color color;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct StreamingControllerZSaver {
    public System.Single streamingMipmapBias;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TerrainZSaver {
    public UnityEngine.TerrainData terrainData;
    public System.Single treeDistance;
    public System.Single treeBillboardDistance;
    public System.Single treeCrossFadeLength;
    public System.Int32 treeMaximumFullLODCount;
    public System.Single detailObjectDistance;
    public System.Single detailObjectDensity;
    public System.Single heightmapPixelError;
    public System.Int32 heightmapMaximumLOD;
    public System.Single basemapDistance;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public System.Boolean freeUnusedRenderingResources;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public UnityEngine.Material materialTemplate;
    public System.Boolean drawHeightmap;
    public System.Boolean allowAutoConnect;
    public System.Int32 groupingID;
    public System.Boolean drawInstanced;
    public UnityEngine.RenderTexture normalmapTexture;
    public System.Boolean drawTreesAndFoliage;
    public UnityEngine.Vector3 patchBoundsMultiplier;
    public System.Single treeLODBiasMultiplier;
    public System.Boolean collectDetailPatches;
    public UnityEngine.TerrainRenderFlags editorRenderFlags;
    public System.Boolean bakeLightProbesForTrees;
    public System.Boolean deringLightProbesForTrees;
    public System.Boolean preserveTreePrototypeLayers;
    public UnityEngine.Terrain leftNeighbor;
    public UnityEngine.Terrain rightNeighbor;
    public UnityEngine.Terrain topNeighbor;
    public UnityEngine.Terrain bottomNeighbor;
    public System.UInt32 renderingLayerMask;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TreeZSaver {
    public UnityEngine.ScriptableObject data;
    public System.Boolean hasSpeedTreeWind;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TerrainColliderZSaver {
    public UnityEngine.TerrainData terrainData;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TextMeshZSaver {
    public System.String text;
    public UnityEngine.Font font;
    public System.Int32 fontSize;
    public UnityEngine.FontStyle fontStyle;
    public System.Single offsetZ;
    public UnityEngine.TextAlignment alignment;
    public UnityEngine.TextAnchor anchor;
    public System.Single characterSize;
    public System.Single lineSpacing;
    public System.Single tabSize;
    public System.Boolean richText;
    public UnityEngine.Color color;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TilemapZSaver {
    public UnityEngine.Grid layoutGrid;
    public UnityEngine.BoundsInt cellBounds;
    public UnityEngine.Bounds localBounds;
    public System.Single animationFrameRate;
    public UnityEngine.Color color;
    public UnityEngine.Vector3Int origin;
    public UnityEngine.Vector3Int size;
    public UnityEngine.Vector3 tileAnchor;
    public UnityEngine.Tilemaps.Tilemap.Orientation orientation;
    public UnityEngine.Matrix4x4 orientationMatrix;
    public UnityEngine.Vector3Int editorPreviewOrigin;
    public UnityEngine.Vector3Int editorPreviewSize;
    public UnityEngine.Vector3 cellSize;
    public UnityEngine.Vector3 cellGap;
    public UnityEngine.GridLayout.CellLayout cellLayout;
    public UnityEngine.GridLayout.CellSwizzle cellSwizzle;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TilemapRendererZSaver {
    public UnityEngine.Vector3Int chunkSize;
    public UnityEngine.Vector3 chunkCullingBounds;
    public System.Int32 maxChunkCount;
    public System.Int32 maxFrameAge;
    public UnityEngine.Tilemaps.TilemapRenderer.SortOrder sortOrder;
    public UnityEngine.Tilemaps.TilemapRenderer.Mode mode;
    public UnityEngine.Tilemaps.TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct TilemapCollider2DZSaver {
    public System.UInt32 maximumTileChangeCount;
    public System.Single extrusionFactor;
    public System.Boolean hasTilemapChanges;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.CompositeCollider2D composite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.Rigidbody2D attachedRigidbody;
    public System.Int32 shapeCount;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Single friction;
    public System.Single bounciness;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CanvasGroupZSaver {
    public System.Single alpha;
    public System.Boolean interactable;
    public System.Boolean blocksRaycasts;
    public System.Boolean ignoreParentGroups;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CanvasRendererZSaver {
    public System.Boolean hasPopInstruction;
    public System.Int32 materialCount;
    public System.Int32 popMaterialCount;
    public System.Int32 absoluteDepth;
    public System.Boolean hasMoved;
    public System.Boolean cullTransparentMesh;
    public System.Boolean hasRectClipping;
    public System.Int32 relativeDepth;
    public System.Boolean cull;
    public UnityEngine.Vector2 clippingSoftness;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct CanvasZSaver {
    public UnityEngine.RenderMode renderMode;
    public System.Boolean isRootCanvas;
    public UnityEngine.Rect pixelRect;
    public System.Single scaleFactor;
    public System.Single referencePixelsPerUnit;
    public System.Boolean overridePixelPerfect;
    public System.Boolean pixelPerfect;
    public System.Single planeDistance;
    public System.Int32 renderOrder;
    public System.Boolean overrideSorting;
    public System.Int32 sortingOrder;
    public System.Int32 targetDisplay;
    public System.Int32 sortingLayerID;
    public System.Int32 cachedSortingLayerValue;
    public UnityEngine.AdditionalCanvasShaderChannels additionalShaderChannels;
    public System.String sortingLayerName;
    public UnityEngine.Canvas rootCanvas;
    public UnityEngine.Vector2 renderingDisplaySize;
    public UnityEngine.Camera worldCamera;
    public System.Single normalizedSortingGridSize;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct VisualEffectZSaver {
    public System.Boolean pause;
    public System.Single playRate;
    public System.UInt32 startSeed;
    public System.Boolean resetSeedOnPlay;
    public System.Int32 initialEventID;
    public System.String initialEventName;
    public System.Boolean culled;
    public UnityEngine.VFX.VisualEffectAsset visualEffectAsset;
    public System.Int32 aliveParticleCount;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct VFXRendererZSaver {
    public UnityEngine.Bounds bounds;
    public System.Boolean enabled;
    public System.Boolean isVisible;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public System.Boolean isPartOfStaticBatch;
    public UnityEngine.Matrix4x4 worldToLocalMatrix;
    public UnityEngine.Matrix4x4 localToWorldMatrix;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct WheelColliderZSaver {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Single suspensionDistance;
    public UnityEngine.JointSpring suspensionSpring;
    public System.Boolean suspensionExpansionLimited;
    public System.Single forceAppPointDistance;
    public System.Single mass;
    public System.Single wheelDampingRate;
    public UnityEngine.WheelFrictionCurve forwardFriction;
    public UnityEngine.WheelFrictionCurve sidewaysFriction;
    public System.Single motorTorque;
    public System.Single brakeTorque;
    public System.Single steerAngle;
    public System.Boolean isGrounded;
    public System.Single rpm;
    public System.Single sprungMass;
    public System.Boolean enabled;
    public UnityEngine.Rigidbody attachedRigidbody;
    public UnityEngine.ArticulationBody attachedArticulationBody;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.Bounds bounds;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct VideoPlayerZSaver {
    public UnityEngine.Video.VideoSource source;
    public System.String url;
    public UnityEngine.Video.VideoClip clip;
    public UnityEngine.Video.VideoRenderMode renderMode;
    public UnityEngine.Camera targetCamera;
    public UnityEngine.RenderTexture targetTexture;
    public UnityEngine.Renderer targetMaterialRenderer;
    public System.String targetMaterialProperty;
    public UnityEngine.Video.VideoAspectRatio aspectRatio;
    public System.Single targetCameraAlpha;
    public UnityEngine.Video.Video3DLayout targetCamera3DLayout;
    public UnityEngine.Texture texture;
    public System.Boolean isPrepared;
    public System.Boolean waitForFirstFrame;
    public System.Boolean playOnAwake;
    public System.Boolean isPlaying;
    public System.Boolean isPaused;
    public System.Boolean canSetTime;
    public System.Double time;
    public System.Int64 frame;
    public System.Double clockTime;
    public System.Boolean canStep;
    public System.Boolean canSetPlaybackSpeed;
    public System.Single playbackSpeed;
    public System.Boolean isLooping;
    public System.Boolean canSetTimeSource;
    public UnityEngine.Video.VideoTimeSource timeSource;
    public UnityEngine.Video.VideoTimeReference timeReference;
    public System.Double externalReferenceTime;
    public System.Boolean canSetSkipOnDrop;
    public System.Boolean skipOnDrop;
    public System.UInt64 frameCount;
    public System.Single frameRate;
    public System.Double length;
    public System.UInt32 width;
    public System.UInt32 height;
    public System.UInt32 pixelAspectRatioNumerator;
    public System.UInt32 pixelAspectRatioDenominator;
    public System.UInt16 audioTrackCount;
    public System.UInt16 controlledAudioTrackCount;
    public UnityEngine.Video.VideoAudioOutputMode audioOutputMode;
    public System.Boolean canSetDirectAudioVolume;
    public System.Boolean sendFrameReadyEvents;
    public System.Boolean enabled;
    public System.Boolean isActiveAndEnabled;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
[System.Serializable]
public struct WindZoneZSaver {
    public UnityEngine.WindZoneMode mode;
    public System.Single radius;
    public System.Single windMain;
    public System.Single windTurbulence;
    public System.Single windPulseMagnitude;
    public System.Single windPulseFrequency;
    public UnityEngine.Transform transform;
    public UnityEngine.GameObject gameObject;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
}
