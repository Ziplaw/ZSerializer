[System.Serializable]
public class NavMeshAgentZSaver : ZSaver.ZSaver<UnityEngine.AI.NavMeshAgent> {
    public UnityEngine.Vector3 destination;
    public System.Single stoppingDistance;
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 nextPosition;
    public System.Single baseOffset;
    public System.Boolean autoTraverseOffMeshLink;
    public System.Boolean autoBraking;
    public System.Boolean autoRepath;
    public System.Boolean isStopped;
    public UnityEngine.AI.NavMeshPath path;
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
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public NavMeshAgentZSaver (UnityEngine.AI.NavMeshAgent NavMeshAgentInstance) : base(NavMeshAgentInstance.gameObject, NavMeshAgentInstance ) {
        destination = NavMeshAgentInstance.destination;
        stoppingDistance = NavMeshAgentInstance.stoppingDistance;
        velocity = NavMeshAgentInstance.velocity;
        nextPosition = NavMeshAgentInstance.nextPosition;
        baseOffset = NavMeshAgentInstance.baseOffset;
        autoTraverseOffMeshLink = NavMeshAgentInstance.autoTraverseOffMeshLink;
        autoBraking = NavMeshAgentInstance.autoBraking;
        autoRepath = NavMeshAgentInstance.autoRepath;
        isStopped = NavMeshAgentInstance.isStopped;
        path = NavMeshAgentInstance.path;
        agentTypeID = NavMeshAgentInstance.agentTypeID;
        areaMask = NavMeshAgentInstance.areaMask;
        speed = NavMeshAgentInstance.speed;
        angularSpeed = NavMeshAgentInstance.angularSpeed;
        acceleration = NavMeshAgentInstance.acceleration;
        updatePosition = NavMeshAgentInstance.updatePosition;
        updateRotation = NavMeshAgentInstance.updateRotation;
        updateUpAxis = NavMeshAgentInstance.updateUpAxis;
        radius = NavMeshAgentInstance.radius;
        height = NavMeshAgentInstance.height;
        obstacleAvoidanceType = NavMeshAgentInstance.obstacleAvoidanceType;
        avoidancePriority = NavMeshAgentInstance.avoidancePriority;
        enabled = NavMeshAgentInstance.enabled;
        hideFlags = NavMeshAgentInstance.hideFlags;

    }
}
[System.Serializable]
public class NavMeshObstacleZSaver : ZSaver.ZSaver<UnityEngine.AI.NavMeshObstacle> {
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
    public UnityEngine.HideFlags hideFlags;
    public NavMeshObstacleZSaver (UnityEngine.AI.NavMeshObstacle NavMeshObstacleInstance) : base(NavMeshObstacleInstance.gameObject, NavMeshObstacleInstance ) {
        height = NavMeshObstacleInstance.height;
        radius = NavMeshObstacleInstance.radius;
        velocity = NavMeshObstacleInstance.velocity;
        carving = NavMeshObstacleInstance.carving;
        carveOnlyStationary = NavMeshObstacleInstance.carveOnlyStationary;
        carvingMoveThreshold = NavMeshObstacleInstance.carvingMoveThreshold;
        carvingTimeToStationary = NavMeshObstacleInstance.carvingTimeToStationary;
        shape = NavMeshObstacleInstance.shape;
        center = NavMeshObstacleInstance.center;
        size = NavMeshObstacleInstance.size;
        enabled = NavMeshObstacleInstance.enabled;
        hideFlags = NavMeshObstacleInstance.hideFlags;

    }
}
[System.Serializable]
public class OffMeshLinkZSaver : ZSaver.ZSaver<UnityEngine.AI.OffMeshLink> {
    public System.Boolean activated;
    public System.Single costOverride;
    public System.Boolean biDirectional;
    public System.Int32 area;
    public System.Boolean autoUpdatePositions;
    public UnityEngine.Transform startTransform;
    public UnityEngine.Transform endTransform;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public OffMeshLinkZSaver (UnityEngine.AI.OffMeshLink OffMeshLinkInstance) : base(OffMeshLinkInstance.gameObject, OffMeshLinkInstance ) {
        activated = OffMeshLinkInstance.activated;
        costOverride = OffMeshLinkInstance.costOverride;
        biDirectional = OffMeshLinkInstance.biDirectional;
        area = OffMeshLinkInstance.area;
        autoUpdatePositions = OffMeshLinkInstance.autoUpdatePositions;
        startTransform = OffMeshLinkInstance.startTransform;
        endTransform = OffMeshLinkInstance.endTransform;
        enabled = OffMeshLinkInstance.enabled;
        hideFlags = OffMeshLinkInstance.hideFlags;

    }
}
[System.Serializable]
public class AnimatorZSaver : ZSaver.ZSaver<UnityEngine.Animator> {
    public UnityEngine.Vector3 rootPosition;
    public UnityEngine.Quaternion rootRotation;
    public System.Boolean applyRootMotion;
    public UnityEngine.AnimatorUpdateMode updateMode;
    public UnityEngine.Vector3 bodyPosition;
    public UnityEngine.Quaternion bodyRotation;
    public System.Boolean stabilizeFeet;
    public System.Single feetPivotActive;
    public System.Single speed;
    public UnityEngine.AnimatorCullingMode cullingMode;
    public System.Single playbackTime;
    public System.Single recorderStartTime;
    public System.Single recorderStopTime;
    public UnityEngine.RuntimeAnimatorController runtimeAnimatorController;
    public UnityEngine.Avatar avatar;
    public System.Boolean layersAffectMassCenter;
    public System.Boolean logWarnings;
    public System.Boolean fireEvents;
    public System.Boolean keepAnimatorControllerStateOnDisable;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AnimatorZSaver (UnityEngine.Animator AnimatorInstance) : base(AnimatorInstance.gameObject, AnimatorInstance ) {
        rootPosition = AnimatorInstance.rootPosition;
        rootRotation = AnimatorInstance.rootRotation;
        applyRootMotion = AnimatorInstance.applyRootMotion;
        updateMode = AnimatorInstance.updateMode;
        bodyPosition = AnimatorInstance.bodyPosition;
        bodyRotation = AnimatorInstance.bodyRotation;
        stabilizeFeet = AnimatorInstance.stabilizeFeet;
        feetPivotActive = AnimatorInstance.feetPivotActive;
        speed = AnimatorInstance.speed;
        cullingMode = AnimatorInstance.cullingMode;
        playbackTime = AnimatorInstance.playbackTime;
        recorderStartTime = AnimatorInstance.recorderStartTime;
        recorderStopTime = AnimatorInstance.recorderStopTime;
        runtimeAnimatorController = AnimatorInstance.runtimeAnimatorController;
        avatar = AnimatorInstance.avatar;
        layersAffectMassCenter = AnimatorInstance.layersAffectMassCenter;
        logWarnings = AnimatorInstance.logWarnings;
        fireEvents = AnimatorInstance.fireEvents;
        keepAnimatorControllerStateOnDisable = AnimatorInstance.keepAnimatorControllerStateOnDisable;
        enabled = AnimatorInstance.enabled;
        hideFlags = AnimatorInstance.hideFlags;

    }
}
[System.Serializable]
public class AnimationZSaver : ZSaver.ZSaver<UnityEngine.Animation> {
    public UnityEngine.AnimationClip clip;
    public System.Boolean playAutomatically;
    public UnityEngine.WrapMode wrapMode;
    public System.Boolean animatePhysics;
    public UnityEngine.AnimationCullingType cullingType;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AnimationZSaver (UnityEngine.Animation AnimationInstance) : base(AnimationInstance.gameObject, AnimationInstance ) {
        clip = AnimationInstance.clip;
        playAutomatically = AnimationInstance.playAutomatically;
        wrapMode = AnimationInstance.wrapMode;
        animatePhysics = AnimationInstance.animatePhysics;
        cullingType = AnimationInstance.cullingType;
        localBounds = AnimationInstance.localBounds;
        enabled = AnimationInstance.enabled;
        hideFlags = AnimationInstance.hideFlags;

    }
}
[System.Serializable]
public class AimConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.AimConstraint> {
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
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AimConstraintZSaver (UnityEngine.Animations.AimConstraint AimConstraintInstance) : base(AimConstraintInstance.gameObject, AimConstraintInstance ) {
        weight = AimConstraintInstance.weight;
        constraintActive = AimConstraintInstance.constraintActive;
        locked = AimConstraintInstance.locked;
        rotationAtRest = AimConstraintInstance.rotationAtRest;
        rotationOffset = AimConstraintInstance.rotationOffset;
        rotationAxis = AimConstraintInstance.rotationAxis;
        aimVector = AimConstraintInstance.aimVector;
        upVector = AimConstraintInstance.upVector;
        worldUpVector = AimConstraintInstance.worldUpVector;
        worldUpObject = AimConstraintInstance.worldUpObject;
        worldUpType = AimConstraintInstance.worldUpType;
        enabled = AimConstraintInstance.enabled;
        hideFlags = AimConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class PositionConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.PositionConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 translationAtRest;
    public UnityEngine.Vector3 translationOffset;
    public UnityEngine.Animations.Axis translationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public PositionConstraintZSaver (UnityEngine.Animations.PositionConstraint PositionConstraintInstance) : base(PositionConstraintInstance.gameObject, PositionConstraintInstance ) {
        weight = PositionConstraintInstance.weight;
        translationAtRest = PositionConstraintInstance.translationAtRest;
        translationOffset = PositionConstraintInstance.translationOffset;
        translationAxis = PositionConstraintInstance.translationAxis;
        constraintActive = PositionConstraintInstance.constraintActive;
        locked = PositionConstraintInstance.locked;
        enabled = PositionConstraintInstance.enabled;
        hideFlags = PositionConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class RotationConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.RotationConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Animations.Axis rotationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public RotationConstraintZSaver (UnityEngine.Animations.RotationConstraint RotationConstraintInstance) : base(RotationConstraintInstance.gameObject, RotationConstraintInstance ) {
        weight = RotationConstraintInstance.weight;
        rotationAtRest = RotationConstraintInstance.rotationAtRest;
        rotationOffset = RotationConstraintInstance.rotationOffset;
        rotationAxis = RotationConstraintInstance.rotationAxis;
        constraintActive = RotationConstraintInstance.constraintActive;
        locked = RotationConstraintInstance.locked;
        enabled = RotationConstraintInstance.enabled;
        hideFlags = RotationConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class ScaleConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.ScaleConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 scaleAtRest;
    public UnityEngine.Vector3 scaleOffset;
    public UnityEngine.Animations.Axis scalingAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ScaleConstraintZSaver (UnityEngine.Animations.ScaleConstraint ScaleConstraintInstance) : base(ScaleConstraintInstance.gameObject, ScaleConstraintInstance ) {
        weight = ScaleConstraintInstance.weight;
        scaleAtRest = ScaleConstraintInstance.scaleAtRest;
        scaleOffset = ScaleConstraintInstance.scaleOffset;
        scalingAxis = ScaleConstraintInstance.scalingAxis;
        constraintActive = ScaleConstraintInstance.constraintActive;
        locked = ScaleConstraintInstance.locked;
        enabled = ScaleConstraintInstance.enabled;
        hideFlags = ScaleConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class LookAtConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.LookAtConstraint> {
    public System.Single weight;
    public System.Single roll;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Transform worldUpObject;
    public System.Boolean useUpObject;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public LookAtConstraintZSaver (UnityEngine.Animations.LookAtConstraint LookAtConstraintInstance) : base(LookAtConstraintInstance.gameObject, LookAtConstraintInstance ) {
        weight = LookAtConstraintInstance.weight;
        roll = LookAtConstraintInstance.roll;
        constraintActive = LookAtConstraintInstance.constraintActive;
        locked = LookAtConstraintInstance.locked;
        rotationAtRest = LookAtConstraintInstance.rotationAtRest;
        rotationOffset = LookAtConstraintInstance.rotationOffset;
        worldUpObject = LookAtConstraintInstance.worldUpObject;
        useUpObject = LookAtConstraintInstance.useUpObject;
        enabled = LookAtConstraintInstance.enabled;
        hideFlags = LookAtConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class ParentConstraintZSaver : ZSaver.ZSaver<UnityEngine.Animations.ParentConstraint> {
    public System.Single weight;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public UnityEngine.Vector3 translationAtRest;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3[] translationOffsets;
    public UnityEngine.Vector3[] rotationOffsets;
    public UnityEngine.Animations.Axis translationAxis;
    public UnityEngine.Animations.Axis rotationAxis;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ParentConstraintZSaver (UnityEngine.Animations.ParentConstraint ParentConstraintInstance) : base(ParentConstraintInstance.gameObject, ParentConstraintInstance ) {
        weight = ParentConstraintInstance.weight;
        constraintActive = ParentConstraintInstance.constraintActive;
        locked = ParentConstraintInstance.locked;
        translationAtRest = ParentConstraintInstance.translationAtRest;
        rotationAtRest = ParentConstraintInstance.rotationAtRest;
        translationOffsets = ParentConstraintInstance.translationOffsets;
        rotationOffsets = ParentConstraintInstance.rotationOffsets;
        translationAxis = ParentConstraintInstance.translationAxis;
        rotationAxis = ParentConstraintInstance.rotationAxis;
        enabled = ParentConstraintInstance.enabled;
        hideFlags = ParentConstraintInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioSourceZSaver : ZSaver.ZSaver<UnityEngine.AudioSource> {
    public System.Single volume;
    public System.Single pitch;
    public System.Single time;
    public System.Int32 timeSamples;
    public UnityEngine.AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup outputAudioMixerGroup;
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
    public UnityEngine.HideFlags hideFlags;
    public AudioSourceZSaver (UnityEngine.AudioSource AudioSourceInstance) : base(AudioSourceInstance.gameObject, AudioSourceInstance ) {
        volume = AudioSourceInstance.volume;
        pitch = AudioSourceInstance.pitch;
        time = AudioSourceInstance.time;
        timeSamples = AudioSourceInstance.timeSamples;
        clip = AudioSourceInstance.clip;
        outputAudioMixerGroup = AudioSourceInstance.outputAudioMixerGroup;
        loop = AudioSourceInstance.loop;
        ignoreListenerVolume = AudioSourceInstance.ignoreListenerVolume;
        playOnAwake = AudioSourceInstance.playOnAwake;
        ignoreListenerPause = AudioSourceInstance.ignoreListenerPause;
        velocityUpdateMode = AudioSourceInstance.velocityUpdateMode;
        panStereo = AudioSourceInstance.panStereo;
        spatialBlend = AudioSourceInstance.spatialBlend;
        spatialize = AudioSourceInstance.spatialize;
        spatializePostEffects = AudioSourceInstance.spatializePostEffects;
        reverbZoneMix = AudioSourceInstance.reverbZoneMix;
        bypassEffects = AudioSourceInstance.bypassEffects;
        bypassListenerEffects = AudioSourceInstance.bypassListenerEffects;
        bypassReverbZones = AudioSourceInstance.bypassReverbZones;
        dopplerLevel = AudioSourceInstance.dopplerLevel;
        spread = AudioSourceInstance.spread;
        priority = AudioSourceInstance.priority;
        mute = AudioSourceInstance.mute;
        minDistance = AudioSourceInstance.minDistance;
        maxDistance = AudioSourceInstance.maxDistance;
        rolloffMode = AudioSourceInstance.rolloffMode;
        enabled = AudioSourceInstance.enabled;
        hideFlags = AudioSourceInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioLowPassFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioLowPassFilter> {
    public UnityEngine.AnimationCurve customCutoffCurve;
    public System.Single cutoffFrequency;
    public System.Single lowpassResonanceQ;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioLowPassFilterZSaver (UnityEngine.AudioLowPassFilter AudioLowPassFilterInstance) : base(AudioLowPassFilterInstance.gameObject, AudioLowPassFilterInstance ) {
        customCutoffCurve = AudioLowPassFilterInstance.customCutoffCurve;
        cutoffFrequency = AudioLowPassFilterInstance.cutoffFrequency;
        lowpassResonanceQ = AudioLowPassFilterInstance.lowpassResonanceQ;
        enabled = AudioLowPassFilterInstance.enabled;
        hideFlags = AudioLowPassFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioHighPassFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioHighPassFilter> {
    public System.Single cutoffFrequency;
    public System.Single highpassResonanceQ;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioHighPassFilterZSaver (UnityEngine.AudioHighPassFilter AudioHighPassFilterInstance) : base(AudioHighPassFilterInstance.gameObject, AudioHighPassFilterInstance ) {
        cutoffFrequency = AudioHighPassFilterInstance.cutoffFrequency;
        highpassResonanceQ = AudioHighPassFilterInstance.highpassResonanceQ;
        enabled = AudioHighPassFilterInstance.enabled;
        hideFlags = AudioHighPassFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioReverbFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioReverbFilter> {
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
    public UnityEngine.HideFlags hideFlags;
    public AudioReverbFilterZSaver (UnityEngine.AudioReverbFilter AudioReverbFilterInstance) : base(AudioReverbFilterInstance.gameObject, AudioReverbFilterInstance ) {
        reverbPreset = AudioReverbFilterInstance.reverbPreset;
        dryLevel = AudioReverbFilterInstance.dryLevel;
        room = AudioReverbFilterInstance.room;
        roomHF = AudioReverbFilterInstance.roomHF;
        decayTime = AudioReverbFilterInstance.decayTime;
        decayHFRatio = AudioReverbFilterInstance.decayHFRatio;
        reflectionsLevel = AudioReverbFilterInstance.reflectionsLevel;
        reflectionsDelay = AudioReverbFilterInstance.reflectionsDelay;
        reverbLevel = AudioReverbFilterInstance.reverbLevel;
        reverbDelay = AudioReverbFilterInstance.reverbDelay;
        diffusion = AudioReverbFilterInstance.diffusion;
        density = AudioReverbFilterInstance.density;
        hfReference = AudioReverbFilterInstance.hfReference;
        roomLF = AudioReverbFilterInstance.roomLF;
        lfReference = AudioReverbFilterInstance.lfReference;
        enabled = AudioReverbFilterInstance.enabled;
        hideFlags = AudioReverbFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioBehaviourZSaver : ZSaver.ZSaver<UnityEngine.AudioBehaviour> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioBehaviourZSaver (UnityEngine.AudioBehaviour AudioBehaviourInstance) : base(AudioBehaviourInstance.gameObject, AudioBehaviourInstance ) {
        enabled = AudioBehaviourInstance.enabled;
        hideFlags = AudioBehaviourInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioListenerZSaver : ZSaver.ZSaver<UnityEngine.AudioListener> {
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioListenerZSaver (UnityEngine.AudioListener AudioListenerInstance) : base(AudioListenerInstance.gameObject, AudioListenerInstance ) {
        velocityUpdateMode = AudioListenerInstance.velocityUpdateMode;
        enabled = AudioListenerInstance.enabled;
        hideFlags = AudioListenerInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioReverbZoneZSaver : ZSaver.ZSaver<UnityEngine.AudioReverbZone> {
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
    public UnityEngine.HideFlags hideFlags;
    public AudioReverbZoneZSaver (UnityEngine.AudioReverbZone AudioReverbZoneInstance) : base(AudioReverbZoneInstance.gameObject, AudioReverbZoneInstance ) {
        minDistance = AudioReverbZoneInstance.minDistance;
        maxDistance = AudioReverbZoneInstance.maxDistance;
        reverbPreset = AudioReverbZoneInstance.reverbPreset;
        room = AudioReverbZoneInstance.room;
        roomHF = AudioReverbZoneInstance.roomHF;
        roomLF = AudioReverbZoneInstance.roomLF;
        decayTime = AudioReverbZoneInstance.decayTime;
        decayHFRatio = AudioReverbZoneInstance.decayHFRatio;
        reflections = AudioReverbZoneInstance.reflections;
        reflectionsDelay = AudioReverbZoneInstance.reflectionsDelay;
        reverb = AudioReverbZoneInstance.reverb;
        reverbDelay = AudioReverbZoneInstance.reverbDelay;
        HFReference = AudioReverbZoneInstance.HFReference;
        LFReference = AudioReverbZoneInstance.LFReference;
        diffusion = AudioReverbZoneInstance.diffusion;
        density = AudioReverbZoneInstance.density;
        enabled = AudioReverbZoneInstance.enabled;
        hideFlags = AudioReverbZoneInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioDistortionFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioDistortionFilter> {
    public System.Single distortionLevel;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioDistortionFilterZSaver (UnityEngine.AudioDistortionFilter AudioDistortionFilterInstance) : base(AudioDistortionFilterInstance.gameObject, AudioDistortionFilterInstance ) {
        distortionLevel = AudioDistortionFilterInstance.distortionLevel;
        enabled = AudioDistortionFilterInstance.enabled;
        hideFlags = AudioDistortionFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioEchoFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioEchoFilter> {
    public System.Single delay;
    public System.Single decayRatio;
    public System.Single dryMix;
    public System.Single wetMix;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioEchoFilterZSaver (UnityEngine.AudioEchoFilter AudioEchoFilterInstance) : base(AudioEchoFilterInstance.gameObject, AudioEchoFilterInstance ) {
        delay = AudioEchoFilterInstance.delay;
        decayRatio = AudioEchoFilterInstance.decayRatio;
        dryMix = AudioEchoFilterInstance.dryMix;
        wetMix = AudioEchoFilterInstance.wetMix;
        enabled = AudioEchoFilterInstance.enabled;
        hideFlags = AudioEchoFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class AudioChorusFilterZSaver : ZSaver.ZSaver<UnityEngine.AudioChorusFilter> {
    public System.Single dryMix;
    public System.Single wetMix1;
    public System.Single wetMix2;
    public System.Single wetMix3;
    public System.Single delay;
    public System.Single rate;
    public System.Single depth;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioChorusFilterZSaver (UnityEngine.AudioChorusFilter AudioChorusFilterInstance) : base(AudioChorusFilterInstance.gameObject, AudioChorusFilterInstance ) {
        dryMix = AudioChorusFilterInstance.dryMix;
        wetMix1 = AudioChorusFilterInstance.wetMix1;
        wetMix2 = AudioChorusFilterInstance.wetMix2;
        wetMix3 = AudioChorusFilterInstance.wetMix3;
        delay = AudioChorusFilterInstance.delay;
        rate = AudioChorusFilterInstance.rate;
        depth = AudioChorusFilterInstance.depth;
        enabled = AudioChorusFilterInstance.enabled;
        hideFlags = AudioChorusFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class ClothZSaver : ZSaver.ZSaver<UnityEngine.Cloth> {
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
    public UnityEngine.HideFlags hideFlags;
    public ClothZSaver (UnityEngine.Cloth ClothInstance) : base(ClothInstance.gameObject, ClothInstance ) {
        coefficients = ClothInstance.coefficients;
        capsuleColliders = ClothInstance.capsuleColliders;
        sphereColliders = ClothInstance.sphereColliders;
        sleepThreshold = ClothInstance.sleepThreshold;
        bendingStiffness = ClothInstance.bendingStiffness;
        stretchingStiffness = ClothInstance.stretchingStiffness;
        damping = ClothInstance.damping;
        externalAcceleration = ClothInstance.externalAcceleration;
        randomAcceleration = ClothInstance.randomAcceleration;
        useGravity = ClothInstance.useGravity;
        enabled = ClothInstance.enabled;
        friction = ClothInstance.friction;
        collisionMassScale = ClothInstance.collisionMassScale;
        enableContinuousCollision = ClothInstance.enableContinuousCollision;
        useVirtualParticles = ClothInstance.useVirtualParticles;
        worldVelocityScale = ClothInstance.worldVelocityScale;
        worldAccelerationScale = ClothInstance.worldAccelerationScale;
        clothSolverFrequency = ClothInstance.clothSolverFrequency;
        useTethers = ClothInstance.useTethers;
        stiffnessFrequency = ClothInstance.stiffnessFrequency;
        selfCollisionDistance = ClothInstance.selfCollisionDistance;
        selfCollisionStiffness = ClothInstance.selfCollisionStiffness;
        hideFlags = ClothInstance.hideFlags;

    }
}
[System.Serializable]
public class CameraZSaver : ZSaver.ZSaver<UnityEngine.Camera> {
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single fieldOfView;
    public UnityEngine.RenderingPath renderingPath;
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
    public UnityEngine.RenderTexture targetTexture;
    public System.Int32 targetDisplay;
    public UnityEngine.Matrix4x4 worldToCameraMatrix;
    public UnityEngine.Matrix4x4 projectionMatrix;
    public UnityEngine.Matrix4x4 nonJitteredProjectionMatrix;
    public System.Boolean useJitteredProjectionMatrixForTransparentRendering;
    public UnityEngine.SceneManagement.Scene scene;
    public System.Single stereoSeparation;
    public System.Single stereoConvergence;
    public UnityEngine.StereoTargetEyeMask stereoTargetEye;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CameraZSaver (UnityEngine.Camera CameraInstance) : base(CameraInstance.gameObject, CameraInstance ) {
        nearClipPlane = CameraInstance.nearClipPlane;
        farClipPlane = CameraInstance.farClipPlane;
        fieldOfView = CameraInstance.fieldOfView;
        renderingPath = CameraInstance.renderingPath;
        allowHDR = CameraInstance.allowHDR;
        allowMSAA = CameraInstance.allowMSAA;
        allowDynamicResolution = CameraInstance.allowDynamicResolution;
        forceIntoRenderTexture = CameraInstance.forceIntoRenderTexture;
        orthographicSize = CameraInstance.orthographicSize;
        orthographic = CameraInstance.orthographic;
        opaqueSortMode = CameraInstance.opaqueSortMode;
        transparencySortMode = CameraInstance.transparencySortMode;
        transparencySortAxis = CameraInstance.transparencySortAxis;
        depth = CameraInstance.depth;
        aspect = CameraInstance.aspect;
        cullingMask = CameraInstance.cullingMask;
        eventMask = CameraInstance.eventMask;
        layerCullSpherical = CameraInstance.layerCullSpherical;
        cameraType = CameraInstance.cameraType;
        overrideSceneCullingMask = CameraInstance.overrideSceneCullingMask;
        layerCullDistances = CameraInstance.layerCullDistances;
        useOcclusionCulling = CameraInstance.useOcclusionCulling;
        cullingMatrix = CameraInstance.cullingMatrix;
        backgroundColor = CameraInstance.backgroundColor;
        clearFlags = CameraInstance.clearFlags;
        depthTextureMode = CameraInstance.depthTextureMode;
        clearStencilAfterLightingPass = CameraInstance.clearStencilAfterLightingPass;
        usePhysicalProperties = CameraInstance.usePhysicalProperties;
        sensorSize = CameraInstance.sensorSize;
        lensShift = CameraInstance.lensShift;
        focalLength = CameraInstance.focalLength;
        gateFit = CameraInstance.gateFit;
        rect = CameraInstance.rect;
        pixelRect = CameraInstance.pixelRect;
        targetTexture = CameraInstance.targetTexture;
        targetDisplay = CameraInstance.targetDisplay;
        worldToCameraMatrix = CameraInstance.worldToCameraMatrix;
        projectionMatrix = CameraInstance.projectionMatrix;
        nonJitteredProjectionMatrix = CameraInstance.nonJitteredProjectionMatrix;
        useJitteredProjectionMatrixForTransparentRendering = CameraInstance.useJitteredProjectionMatrixForTransparentRendering;
        scene = CameraInstance.scene;
        stereoSeparation = CameraInstance.stereoSeparation;
        stereoConvergence = CameraInstance.stereoConvergence;
        stereoTargetEye = CameraInstance.stereoTargetEye;
        enabled = CameraInstance.enabled;
        hideFlags = CameraInstance.hideFlags;

    }
}
[System.Serializable]
public class FlareLayerZSaver : ZSaver.ZSaver<UnityEngine.FlareLayer> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public FlareLayerZSaver (UnityEngine.FlareLayer FlareLayerInstance) : base(FlareLayerInstance.gameObject, FlareLayerInstance ) {
        enabled = FlareLayerInstance.enabled;
        hideFlags = FlareLayerInstance.hideFlags;

    }
}
[System.Serializable]
public class ReflectionProbeZSaver : ZSaver.ZSaver<UnityEngine.ReflectionProbe> {
    public UnityEngine.Vector3 size;
    public UnityEngine.Vector3 center;
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single intensity;
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
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ReflectionProbeZSaver (UnityEngine.ReflectionProbe ReflectionProbeInstance) : base(ReflectionProbeInstance.gameObject, ReflectionProbeInstance ) {
        size = ReflectionProbeInstance.size;
        center = ReflectionProbeInstance.center;
        nearClipPlane = ReflectionProbeInstance.nearClipPlane;
        farClipPlane = ReflectionProbeInstance.farClipPlane;
        intensity = ReflectionProbeInstance.intensity;
        hdr = ReflectionProbeInstance.hdr;
        renderDynamicObjects = ReflectionProbeInstance.renderDynamicObjects;
        shadowDistance = ReflectionProbeInstance.shadowDistance;
        resolution = ReflectionProbeInstance.resolution;
        cullingMask = ReflectionProbeInstance.cullingMask;
        clearFlags = ReflectionProbeInstance.clearFlags;
        backgroundColor = ReflectionProbeInstance.backgroundColor;
        blendDistance = ReflectionProbeInstance.blendDistance;
        boxProjection = ReflectionProbeInstance.boxProjection;
        mode = ReflectionProbeInstance.mode;
        importance = ReflectionProbeInstance.importance;
        refreshMode = ReflectionProbeInstance.refreshMode;
        timeSlicingMode = ReflectionProbeInstance.timeSlicingMode;
        bakedTexture = ReflectionProbeInstance.bakedTexture;
        customBakedTexture = ReflectionProbeInstance.customBakedTexture;
        realtimeTexture = ReflectionProbeInstance.realtimeTexture;
        enabled = ReflectionProbeInstance.enabled;
        hideFlags = ReflectionProbeInstance.hideFlags;

    }
}
[System.Serializable]
public class BillboardRendererZSaver : ZSaver.ZSaver<UnityEngine.BillboardRenderer> {
    public UnityEngine.BillboardAsset billboard;
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public BillboardRendererZSaver (UnityEngine.BillboardRenderer BillboardRendererInstance) : base(BillboardRendererInstance.gameObject, BillboardRendererInstance ) {
        billboard = BillboardRendererInstance.billboard;
        enabled = BillboardRendererInstance.enabled;
        shadowCastingMode = BillboardRendererInstance.shadowCastingMode;
        receiveShadows = BillboardRendererInstance.receiveShadows;
        forceRenderingOff = BillboardRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = BillboardRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = BillboardRendererInstance.lightProbeUsage;
        reflectionProbeUsage = BillboardRendererInstance.reflectionProbeUsage;
        renderingLayerMask = BillboardRendererInstance.renderingLayerMask;
        rendererPriority = BillboardRendererInstance.rendererPriority;
        rayTracingMode = BillboardRendererInstance.rayTracingMode;
        sortingLayerName = BillboardRendererInstance.sortingLayerName;
        sortingLayerID = BillboardRendererInstance.sortingLayerID;
        sortingOrder = BillboardRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = BillboardRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = BillboardRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = BillboardRendererInstance.probeAnchor;
        lightmapIndex = BillboardRendererInstance.lightmapIndex;
        realtimeLightmapIndex = BillboardRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = BillboardRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = BillboardRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = BillboardRendererInstance.sharedMaterials;
        hideFlags = BillboardRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class RendererZSaver : ZSaver.ZSaver<UnityEngine.Renderer> {
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public RendererZSaver (UnityEngine.Renderer RendererInstance) : base(RendererInstance.gameObject, RendererInstance ) {
        enabled = RendererInstance.enabled;
        shadowCastingMode = RendererInstance.shadowCastingMode;
        receiveShadows = RendererInstance.receiveShadows;
        forceRenderingOff = RendererInstance.forceRenderingOff;
        motionVectorGenerationMode = RendererInstance.motionVectorGenerationMode;
        lightProbeUsage = RendererInstance.lightProbeUsage;
        reflectionProbeUsage = RendererInstance.reflectionProbeUsage;
        renderingLayerMask = RendererInstance.renderingLayerMask;
        rendererPriority = RendererInstance.rendererPriority;
        rayTracingMode = RendererInstance.rayTracingMode;
        sortingLayerName = RendererInstance.sortingLayerName;
        sortingLayerID = RendererInstance.sortingLayerID;
        sortingOrder = RendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = RendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = RendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = RendererInstance.probeAnchor;
        lightmapIndex = RendererInstance.lightmapIndex;
        realtimeLightmapIndex = RendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = RendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = RendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = RendererInstance.sharedMaterials;
        hideFlags = RendererInstance.hideFlags;

    }
}
[System.Serializable]
public class ProjectorZSaver : ZSaver.ZSaver<UnityEngine.Projector> {
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single fieldOfView;
    public System.Single aspectRatio;
    public System.Boolean orthographic;
    public System.Single orthographicSize;
    public System.Int32 ignoreLayers;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ProjectorZSaver (UnityEngine.Projector ProjectorInstance) : base(ProjectorInstance.gameObject, ProjectorInstance ) {
        nearClipPlane = ProjectorInstance.nearClipPlane;
        farClipPlane = ProjectorInstance.farClipPlane;
        fieldOfView = ProjectorInstance.fieldOfView;
        aspectRatio = ProjectorInstance.aspectRatio;
        orthographic = ProjectorInstance.orthographic;
        orthographicSize = ProjectorInstance.orthographicSize;
        ignoreLayers = ProjectorInstance.ignoreLayers;
        enabled = ProjectorInstance.enabled;
        hideFlags = ProjectorInstance.hideFlags;

    }
}
[System.Serializable]
public class TrailRendererZSaver : ZSaver.ZSaver<UnityEngine.TrailRenderer> {
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
    public System.Single shadowBias;
    public System.Boolean generateLightingData;
    public UnityEngine.LineTextureMode textureMode;
    public UnityEngine.LineAlignment alignment;
    public UnityEngine.AnimationCurve widthCurve;
    public UnityEngine.Gradient colorGradient;
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public TrailRendererZSaver (UnityEngine.TrailRenderer TrailRendererInstance) : base(TrailRendererInstance.gameObject, TrailRendererInstance ) {
        time = TrailRendererInstance.time;
        startWidth = TrailRendererInstance.startWidth;
        endWidth = TrailRendererInstance.endWidth;
        widthMultiplier = TrailRendererInstance.widthMultiplier;
        autodestruct = TrailRendererInstance.autodestruct;
        emitting = TrailRendererInstance.emitting;
        numCornerVertices = TrailRendererInstance.numCornerVertices;
        numCapVertices = TrailRendererInstance.numCapVertices;
        minVertexDistance = TrailRendererInstance.minVertexDistance;
        startColor = TrailRendererInstance.startColor;
        endColor = TrailRendererInstance.endColor;
        shadowBias = TrailRendererInstance.shadowBias;
        generateLightingData = TrailRendererInstance.generateLightingData;
        textureMode = TrailRendererInstance.textureMode;
        alignment = TrailRendererInstance.alignment;
        widthCurve = TrailRendererInstance.widthCurve;
        colorGradient = TrailRendererInstance.colorGradient;
        enabled = TrailRendererInstance.enabled;
        shadowCastingMode = TrailRendererInstance.shadowCastingMode;
        receiveShadows = TrailRendererInstance.receiveShadows;
        forceRenderingOff = TrailRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = TrailRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = TrailRendererInstance.lightProbeUsage;
        reflectionProbeUsage = TrailRendererInstance.reflectionProbeUsage;
        renderingLayerMask = TrailRendererInstance.renderingLayerMask;
        rendererPriority = TrailRendererInstance.rendererPriority;
        rayTracingMode = TrailRendererInstance.rayTracingMode;
        sortingLayerName = TrailRendererInstance.sortingLayerName;
        sortingLayerID = TrailRendererInstance.sortingLayerID;
        sortingOrder = TrailRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = TrailRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = TrailRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = TrailRendererInstance.probeAnchor;
        lightmapIndex = TrailRendererInstance.lightmapIndex;
        realtimeLightmapIndex = TrailRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = TrailRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = TrailRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = TrailRendererInstance.sharedMaterials;
        hideFlags = TrailRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class LineRendererZSaver : ZSaver.ZSaver<UnityEngine.LineRenderer> {
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
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public LineRendererZSaver (UnityEngine.LineRenderer LineRendererInstance) : base(LineRendererInstance.gameObject, LineRendererInstance ) {
        startWidth = LineRendererInstance.startWidth;
        endWidth = LineRendererInstance.endWidth;
        widthMultiplier = LineRendererInstance.widthMultiplier;
        numCornerVertices = LineRendererInstance.numCornerVertices;
        numCapVertices = LineRendererInstance.numCapVertices;
        useWorldSpace = LineRendererInstance.useWorldSpace;
        loop = LineRendererInstance.loop;
        startColor = LineRendererInstance.startColor;
        endColor = LineRendererInstance.endColor;
        positionCount = LineRendererInstance.positionCount;
        shadowBias = LineRendererInstance.shadowBias;
        generateLightingData = LineRendererInstance.generateLightingData;
        textureMode = LineRendererInstance.textureMode;
        alignment = LineRendererInstance.alignment;
        widthCurve = LineRendererInstance.widthCurve;
        colorGradient = LineRendererInstance.colorGradient;
        enabled = LineRendererInstance.enabled;
        shadowCastingMode = LineRendererInstance.shadowCastingMode;
        receiveShadows = LineRendererInstance.receiveShadows;
        forceRenderingOff = LineRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = LineRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = LineRendererInstance.lightProbeUsage;
        reflectionProbeUsage = LineRendererInstance.reflectionProbeUsage;
        renderingLayerMask = LineRendererInstance.renderingLayerMask;
        rendererPriority = LineRendererInstance.rendererPriority;
        rayTracingMode = LineRendererInstance.rayTracingMode;
        sortingLayerName = LineRendererInstance.sortingLayerName;
        sortingLayerID = LineRendererInstance.sortingLayerID;
        sortingOrder = LineRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = LineRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = LineRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = LineRendererInstance.probeAnchor;
        lightmapIndex = LineRendererInstance.lightmapIndex;
        realtimeLightmapIndex = LineRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = LineRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = LineRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = LineRendererInstance.sharedMaterials;
        hideFlags = LineRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class OcclusionPortalZSaver : ZSaver.ZSaver<UnityEngine.OcclusionPortal> {
    public System.Boolean open;
    public UnityEngine.HideFlags hideFlags;
    public OcclusionPortalZSaver (UnityEngine.OcclusionPortal OcclusionPortalInstance) : base(OcclusionPortalInstance.gameObject, OcclusionPortalInstance ) {
        open = OcclusionPortalInstance.open;
        hideFlags = OcclusionPortalInstance.hideFlags;

    }
}
[System.Serializable]
public class OcclusionAreaZSaver : ZSaver.ZSaver<UnityEngine.OcclusionArea> {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public UnityEngine.HideFlags hideFlags;
    public OcclusionAreaZSaver (UnityEngine.OcclusionArea OcclusionAreaInstance) : base(OcclusionAreaInstance.gameObject, OcclusionAreaInstance ) {
        center = OcclusionAreaInstance.center;
        size = OcclusionAreaInstance.size;
        hideFlags = OcclusionAreaInstance.hideFlags;

    }
}
[System.Serializable]
public class LensFlareZSaver : ZSaver.ZSaver<UnityEngine.LensFlare> {
    public System.Single brightness;
    public System.Single fadeSpeed;
    public UnityEngine.Color color;
    public UnityEngine.Flare flare;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public LensFlareZSaver (UnityEngine.LensFlare LensFlareInstance) : base(LensFlareInstance.gameObject, LensFlareInstance ) {
        brightness = LensFlareInstance.brightness;
        fadeSpeed = LensFlareInstance.fadeSpeed;
        color = LensFlareInstance.color;
        flare = LensFlareInstance.flare;
        enabled = LensFlareInstance.enabled;
        hideFlags = LensFlareInstance.hideFlags;

    }
}
[System.Serializable]
public class LightZSaver : ZSaver.ZSaver<UnityEngine.Light> {
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
    public UnityEngine.LightShadows shadows;
    public System.Single shadowStrength;
    public UnityEngine.Rendering.LightShadowResolution shadowResolution;
    public System.Single[] layerShadowCullDistances;
    public System.Single cookieSize;
    public UnityEngine.Texture cookie;
    public UnityEngine.LightRenderMode renderMode;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public LightZSaver (UnityEngine.Light LightInstance) : base(LightInstance.gameObject, LightInstance ) {
        type = LightInstance.type;
        shape = LightInstance.shape;
        spotAngle = LightInstance.spotAngle;
        innerSpotAngle = LightInstance.innerSpotAngle;
        color = LightInstance.color;
        colorTemperature = LightInstance.colorTemperature;
        useColorTemperature = LightInstance.useColorTemperature;
        intensity = LightInstance.intensity;
        bounceIntensity = LightInstance.bounceIntensity;
        useBoundingSphereOverride = LightInstance.useBoundingSphereOverride;
        boundingSphereOverride = LightInstance.boundingSphereOverride;
        useViewFrustumForShadowCasterCull = LightInstance.useViewFrustumForShadowCasterCull;
        shadowCustomResolution = LightInstance.shadowCustomResolution;
        shadowBias = LightInstance.shadowBias;
        shadowNormalBias = LightInstance.shadowNormalBias;
        shadowNearPlane = LightInstance.shadowNearPlane;
        useShadowMatrixOverride = LightInstance.useShadowMatrixOverride;
        shadowMatrixOverride = LightInstance.shadowMatrixOverride;
        range = LightInstance.range;
        flare = LightInstance.flare;
        bakingOutput = LightInstance.bakingOutput;
        cullingMask = LightInstance.cullingMask;
        renderingLayerMask = LightInstance.renderingLayerMask;
        lightShadowCasterMode = LightInstance.lightShadowCasterMode;
        shadows = LightInstance.shadows;
        shadowStrength = LightInstance.shadowStrength;
        shadowResolution = LightInstance.shadowResolution;
        layerShadowCullDistances = LightInstance.layerShadowCullDistances;
        cookieSize = LightInstance.cookieSize;
        cookie = LightInstance.cookie;
        renderMode = LightInstance.renderMode;
        enabled = LightInstance.enabled;
        hideFlags = LightInstance.hideFlags;

    }
}
[System.Serializable]
public class SkyboxZSaver : ZSaver.ZSaver<UnityEngine.Skybox> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public SkyboxZSaver (UnityEngine.Skybox SkyboxInstance) : base(SkyboxInstance.gameObject, SkyboxInstance ) {
        enabled = SkyboxInstance.enabled;
        hideFlags = SkyboxInstance.hideFlags;

    }
}
[System.Serializable]
public class MeshFilterZSaver : ZSaver.ZSaver<UnityEngine.MeshFilter> {
    public UnityEngine.Mesh sharedMesh;
    public UnityEngine.HideFlags hideFlags;
    public MeshFilterZSaver (UnityEngine.MeshFilter MeshFilterInstance) : base(MeshFilterInstance.gameObject, MeshFilterInstance ) {
        sharedMesh = MeshFilterInstance.sharedMesh;
        hideFlags = MeshFilterInstance.hideFlags;

    }
}
[System.Serializable]
public class LightProbeProxyVolumeZSaver : ZSaver.ZSaver<UnityEngine.LightProbeProxyVolume> {
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
    public UnityEngine.HideFlags hideFlags;
    public LightProbeProxyVolumeZSaver (UnityEngine.LightProbeProxyVolume LightProbeProxyVolumeInstance) : base(LightProbeProxyVolumeInstance.gameObject, LightProbeProxyVolumeInstance ) {
        sizeCustom = LightProbeProxyVolumeInstance.sizeCustom;
        originCustom = LightProbeProxyVolumeInstance.originCustom;
        probeDensity = LightProbeProxyVolumeInstance.probeDensity;
        gridResolutionX = LightProbeProxyVolumeInstance.gridResolutionX;
        gridResolutionY = LightProbeProxyVolumeInstance.gridResolutionY;
        gridResolutionZ = LightProbeProxyVolumeInstance.gridResolutionZ;
        boundingBoxMode = LightProbeProxyVolumeInstance.boundingBoxMode;
        resolutionMode = LightProbeProxyVolumeInstance.resolutionMode;
        probePositionMode = LightProbeProxyVolumeInstance.probePositionMode;
        refreshMode = LightProbeProxyVolumeInstance.refreshMode;
        qualityMode = LightProbeProxyVolumeInstance.qualityMode;
        dataFormat = LightProbeProxyVolumeInstance.dataFormat;
        enabled = LightProbeProxyVolumeInstance.enabled;
        hideFlags = LightProbeProxyVolumeInstance.hideFlags;

    }
}
[System.Serializable]
public class LightProbeGroupZSaver : ZSaver.ZSaver<UnityEngine.LightProbeGroup> {
    public UnityEngine.Vector3[] probePositions;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public LightProbeGroupZSaver (UnityEngine.LightProbeGroup LightProbeGroupInstance) : base(LightProbeGroupInstance.gameObject, LightProbeGroupInstance ) {
        probePositions = LightProbeGroupInstance.probePositions;
        enabled = LightProbeGroupInstance.enabled;
        hideFlags = LightProbeGroupInstance.hideFlags;

    }
}
[System.Serializable]
public class LODGroupZSaver : ZSaver.ZSaver<UnityEngine.LODGroup> {
    public UnityEngine.Vector3 localReferencePoint;
    public System.Single size;
    public UnityEngine.LODFadeMode fadeMode;
    public System.Boolean animateCrossFading;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public LODGroupZSaver (UnityEngine.LODGroup LODGroupInstance) : base(LODGroupInstance.gameObject, LODGroupInstance ) {
        localReferencePoint = LODGroupInstance.localReferencePoint;
        size = LODGroupInstance.size;
        fadeMode = LODGroupInstance.fadeMode;
        animateCrossFading = LODGroupInstance.animateCrossFading;
        enabled = LODGroupInstance.enabled;
        hideFlags = LODGroupInstance.hideFlags;

    }
}
[System.Serializable]
public class BehaviourZSaver : ZSaver.ZSaver<UnityEngine.Behaviour> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public BehaviourZSaver (UnityEngine.Behaviour BehaviourInstance) : base(BehaviourInstance.gameObject, BehaviourInstance ) {
        enabled = BehaviourInstance.enabled;
        hideFlags = BehaviourInstance.hideFlags;

    }
}
[System.Serializable]
public class RectTransformZSaver : ZSaver.ZSaver<UnityEngine.RectTransform> {
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
    public System.Boolean hasChanged;
    public System.Int32 hierarchyCapacity;
    public UnityEngine.HideFlags hideFlags;
    public RectTransformZSaver (UnityEngine.RectTransform RectTransformInstance) : base(RectTransformInstance.gameObject, RectTransformInstance ) {
        anchorMin = RectTransformInstance.anchorMin;
        anchorMax = RectTransformInstance.anchorMax;
        anchoredPosition = RectTransformInstance.anchoredPosition;
        sizeDelta = RectTransformInstance.sizeDelta;
        pivot = RectTransformInstance.pivot;
        anchoredPosition3D = RectTransformInstance.anchoredPosition3D;
        offsetMin = RectTransformInstance.offsetMin;
        offsetMax = RectTransformInstance.offsetMax;
        position = RectTransformInstance.position;
        localPosition = RectTransformInstance.localPosition;
        eulerAngles = RectTransformInstance.eulerAngles;
        localEulerAngles = RectTransformInstance.localEulerAngles;
        right = RectTransformInstance.right;
        up = RectTransformInstance.up;
        forward = RectTransformInstance.forward;
        rotation = RectTransformInstance.rotation;
        localRotation = RectTransformInstance.localRotation;
        localScale = RectTransformInstance.localScale;
        parent = RectTransformInstance.parent;
        hasChanged = RectTransformInstance.hasChanged;
        hierarchyCapacity = RectTransformInstance.hierarchyCapacity;
        hideFlags = RectTransformInstance.hideFlags;

    }
}
[System.Serializable]
public class SortingGroupZSaver : ZSaver.ZSaver<UnityEngine.Rendering.SortingGroup> {
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public SortingGroupZSaver (UnityEngine.Rendering.SortingGroup SortingGroupInstance) : base(SortingGroupInstance.gameObject, SortingGroupInstance ) {
        sortingLayerName = SortingGroupInstance.sortingLayerName;
        sortingLayerID = SortingGroupInstance.sortingLayerID;
        sortingOrder = SortingGroupInstance.sortingOrder;
        enabled = SortingGroupInstance.enabled;
        hideFlags = SortingGroupInstance.hideFlags;

    }
}
[System.Serializable]
public class PlayableDirectorZSaver : ZSaver.ZSaver<UnityEngine.Playables.PlayableDirector> {
    public UnityEngine.Playables.DirectorWrapMode extrapolationMode;
    public UnityEngine.Playables.PlayableAsset playableAsset;
    public System.Boolean playOnAwake;
    public UnityEngine.Playables.DirectorUpdateMode timeUpdateMode;
    public System.Double time;
    public System.Double initialTime;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public PlayableDirectorZSaver (UnityEngine.Playables.PlayableDirector PlayableDirectorInstance) : base(PlayableDirectorInstance.gameObject, PlayableDirectorInstance ) {
        extrapolationMode = PlayableDirectorInstance.extrapolationMode;
        playableAsset = PlayableDirectorInstance.playableAsset;
        playOnAwake = PlayableDirectorInstance.playOnAwake;
        timeUpdateMode = PlayableDirectorInstance.timeUpdateMode;
        time = PlayableDirectorInstance.time;
        initialTime = PlayableDirectorInstance.initialTime;
        enabled = PlayableDirectorInstance.enabled;
        hideFlags = PlayableDirectorInstance.hideFlags;

    }
}
[System.Serializable]
public class GridZSaver : ZSaver.ZSaver<UnityEngine.Grid> {
    public UnityEngine.Vector3 cellSize;
    public UnityEngine.Vector3 cellGap;
    public UnityEngine.GridLayout.CellLayout cellLayout;
    public UnityEngine.GridLayout.CellSwizzle cellSwizzle;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public GridZSaver (UnityEngine.Grid GridInstance) : base(GridInstance.gameObject, GridInstance ) {
        cellSize = GridInstance.cellSize;
        cellGap = GridInstance.cellGap;
        cellLayout = GridInstance.cellLayout;
        cellSwizzle = GridInstance.cellSwizzle;
        enabled = GridInstance.enabled;
        hideFlags = GridInstance.hideFlags;

    }
}
[System.Serializable]
public class GridLayoutZSaver : ZSaver.ZSaver<UnityEngine.GridLayout> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public GridLayoutZSaver (UnityEngine.GridLayout GridLayoutInstance) : base(GridLayoutInstance.gameObject, GridLayoutInstance ) {
        enabled = GridLayoutInstance.enabled;
        hideFlags = GridLayoutInstance.hideFlags;

    }
}
[System.Serializable]
public class ParticleSystemZSaver : ZSaver.ZSaver<UnityEngine.ParticleSystem> {
    public System.Single time;
    public System.UInt32 randomSeed;
    public System.Boolean useAutoRandomSeed;
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemZSaver (UnityEngine.ParticleSystem ParticleSystemInstance) : base(ParticleSystemInstance.gameObject, ParticleSystemInstance ) {
        time = ParticleSystemInstance.time;
        randomSeed = ParticleSystemInstance.randomSeed;
        useAutoRandomSeed = ParticleSystemInstance.useAutoRandomSeed;
        hideFlags = ParticleSystemInstance.hideFlags;

    }
}
[System.Serializable]
public class ParticleSystemRendererZSaver : ZSaver.ZSaver<UnityEngine.ParticleSystemRenderer> {
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
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemRendererZSaver (UnityEngine.ParticleSystemRenderer ParticleSystemRendererInstance) : base(ParticleSystemRendererInstance.gameObject, ParticleSystemRendererInstance ) {
        alignment = ParticleSystemRendererInstance.alignment;
        renderMode = ParticleSystemRendererInstance.renderMode;
        sortMode = ParticleSystemRendererInstance.sortMode;
        lengthScale = ParticleSystemRendererInstance.lengthScale;
        velocityScale = ParticleSystemRendererInstance.velocityScale;
        cameraVelocityScale = ParticleSystemRendererInstance.cameraVelocityScale;
        normalDirection = ParticleSystemRendererInstance.normalDirection;
        shadowBias = ParticleSystemRendererInstance.shadowBias;
        sortingFudge = ParticleSystemRendererInstance.sortingFudge;
        minParticleSize = ParticleSystemRendererInstance.minParticleSize;
        maxParticleSize = ParticleSystemRendererInstance.maxParticleSize;
        pivot = ParticleSystemRendererInstance.pivot;
        flip = ParticleSystemRendererInstance.flip;
        maskInteraction = ParticleSystemRendererInstance.maskInteraction;
        trailMaterial = ParticleSystemRendererInstance.trailMaterial;
        enableGPUInstancing = ParticleSystemRendererInstance.enableGPUInstancing;
        allowRoll = ParticleSystemRendererInstance.allowRoll;
        freeformStretching = ParticleSystemRendererInstance.freeformStretching;
        rotateWithStretchDirection = ParticleSystemRendererInstance.rotateWithStretchDirection;
        enabled = ParticleSystemRendererInstance.enabled;
        shadowCastingMode = ParticleSystemRendererInstance.shadowCastingMode;
        receiveShadows = ParticleSystemRendererInstance.receiveShadows;
        forceRenderingOff = ParticleSystemRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = ParticleSystemRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = ParticleSystemRendererInstance.lightProbeUsage;
        reflectionProbeUsage = ParticleSystemRendererInstance.reflectionProbeUsage;
        renderingLayerMask = ParticleSystemRendererInstance.renderingLayerMask;
        rendererPriority = ParticleSystemRendererInstance.rendererPriority;
        rayTracingMode = ParticleSystemRendererInstance.rayTracingMode;
        sortingLayerName = ParticleSystemRendererInstance.sortingLayerName;
        sortingLayerID = ParticleSystemRendererInstance.sortingLayerID;
        sortingOrder = ParticleSystemRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = ParticleSystemRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = ParticleSystemRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = ParticleSystemRendererInstance.probeAnchor;
        lightmapIndex = ParticleSystemRendererInstance.lightmapIndex;
        realtimeLightmapIndex = ParticleSystemRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = ParticleSystemRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = ParticleSystemRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = ParticleSystemRendererInstance.sharedMaterials;
        hideFlags = ParticleSystemRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class ParticleSystemForceFieldZSaver : ZSaver.ZSaver<UnityEngine.ParticleSystemForceField> {
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
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemForceFieldZSaver (UnityEngine.ParticleSystemForceField ParticleSystemForceFieldInstance) : base(ParticleSystemForceFieldInstance.gameObject, ParticleSystemForceFieldInstance ) {
        shape = ParticleSystemForceFieldInstance.shape;
        startRange = ParticleSystemForceFieldInstance.startRange;
        endRange = ParticleSystemForceFieldInstance.endRange;
        length = ParticleSystemForceFieldInstance.length;
        gravityFocus = ParticleSystemForceFieldInstance.gravityFocus;
        rotationRandomness = ParticleSystemForceFieldInstance.rotationRandomness;
        multiplyDragByParticleSize = ParticleSystemForceFieldInstance.multiplyDragByParticleSize;
        multiplyDragByParticleVelocity = ParticleSystemForceFieldInstance.multiplyDragByParticleVelocity;
        vectorField = ParticleSystemForceFieldInstance.vectorField;
        directionX = ParticleSystemForceFieldInstance.directionX;
        directionY = ParticleSystemForceFieldInstance.directionY;
        directionZ = ParticleSystemForceFieldInstance.directionZ;
        gravity = ParticleSystemForceFieldInstance.gravity;
        rotationSpeed = ParticleSystemForceFieldInstance.rotationSpeed;
        rotationAttraction = ParticleSystemForceFieldInstance.rotationAttraction;
        drag = ParticleSystemForceFieldInstance.drag;
        vectorFieldSpeed = ParticleSystemForceFieldInstance.vectorFieldSpeed;
        vectorFieldAttraction = ParticleSystemForceFieldInstance.vectorFieldAttraction;
        hideFlags = ParticleSystemForceFieldInstance.hideFlags;

    }
}
[System.Serializable]
public class RigidbodyZSaver : ZSaver.ZSaver<UnityEngine.Rigidbody> {
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
    public UnityEngine.HideFlags hideFlags;
    public RigidbodyZSaver (UnityEngine.Rigidbody RigidbodyInstance) : base(RigidbodyInstance.gameObject, RigidbodyInstance ) {
        velocity = RigidbodyInstance.velocity;
        angularVelocity = RigidbodyInstance.angularVelocity;
        drag = RigidbodyInstance.drag;
        angularDrag = RigidbodyInstance.angularDrag;
        mass = RigidbodyInstance.mass;
        useGravity = RigidbodyInstance.useGravity;
        maxDepenetrationVelocity = RigidbodyInstance.maxDepenetrationVelocity;
        isKinematic = RigidbodyInstance.isKinematic;
        freezeRotation = RigidbodyInstance.freezeRotation;
        constraints = RigidbodyInstance.constraints;
        collisionDetectionMode = RigidbodyInstance.collisionDetectionMode;
        centerOfMass = RigidbodyInstance.centerOfMass;
        inertiaTensorRotation = RigidbodyInstance.inertiaTensorRotation;
        inertiaTensor = RigidbodyInstance.inertiaTensor;
        detectCollisions = RigidbodyInstance.detectCollisions;
        position = RigidbodyInstance.position;
        rotation = RigidbodyInstance.rotation;
        interpolation = RigidbodyInstance.interpolation;
        solverIterations = RigidbodyInstance.solverIterations;
        sleepThreshold = RigidbodyInstance.sleepThreshold;
        maxAngularVelocity = RigidbodyInstance.maxAngularVelocity;
        solverVelocityIterations = RigidbodyInstance.solverVelocityIterations;
        hideFlags = RigidbodyInstance.hideFlags;

    }
}
[System.Serializable]
public class ColliderZSaver : ZSaver.ZSaver<UnityEngine.Collider> {
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public ColliderZSaver (UnityEngine.Collider ColliderInstance) : base(ColliderInstance.gameObject, ColliderInstance ) {
        enabled = ColliderInstance.enabled;
        isTrigger = ColliderInstance.isTrigger;
        contactOffset = ColliderInstance.contactOffset;
        hideFlags = ColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class CharacterControllerZSaver : ZSaver.ZSaver<UnityEngine.CharacterController> {
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
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public CharacterControllerZSaver (UnityEngine.CharacterController CharacterControllerInstance) : base(CharacterControllerInstance.gameObject, CharacterControllerInstance ) {
        radius = CharacterControllerInstance.radius;
        height = CharacterControllerInstance.height;
        center = CharacterControllerInstance.center;
        slopeLimit = CharacterControllerInstance.slopeLimit;
        stepOffset = CharacterControllerInstance.stepOffset;
        skinWidth = CharacterControllerInstance.skinWidth;
        minMoveDistance = CharacterControllerInstance.minMoveDistance;
        detectCollisions = CharacterControllerInstance.detectCollisions;
        enableOverlapRecovery = CharacterControllerInstance.enableOverlapRecovery;
        enabled = CharacterControllerInstance.enabled;
        isTrigger = CharacterControllerInstance.isTrigger;
        contactOffset = CharacterControllerInstance.contactOffset;
        hideFlags = CharacterControllerInstance.hideFlags;

    }
}
[System.Serializable]
public class MeshColliderZSaver : ZSaver.ZSaver<UnityEngine.MeshCollider> {
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean convex;
    public UnityEngine.MeshColliderCookingOptions cookingOptions;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public MeshColliderZSaver (UnityEngine.MeshCollider MeshColliderInstance) : base(MeshColliderInstance.gameObject, MeshColliderInstance ) {
        sharedMesh = MeshColliderInstance.sharedMesh;
        convex = MeshColliderInstance.convex;
        cookingOptions = MeshColliderInstance.cookingOptions;
        enabled = MeshColliderInstance.enabled;
        isTrigger = MeshColliderInstance.isTrigger;
        contactOffset = MeshColliderInstance.contactOffset;
        hideFlags = MeshColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class CapsuleColliderZSaver : ZSaver.ZSaver<UnityEngine.CapsuleCollider> {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Single height;
    public System.Int32 direction;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public CapsuleColliderZSaver (UnityEngine.CapsuleCollider CapsuleColliderInstance) : base(CapsuleColliderInstance.gameObject, CapsuleColliderInstance ) {
        center = CapsuleColliderInstance.center;
        radius = CapsuleColliderInstance.radius;
        height = CapsuleColliderInstance.height;
        direction = CapsuleColliderInstance.direction;
        enabled = CapsuleColliderInstance.enabled;
        isTrigger = CapsuleColliderInstance.isTrigger;
        contactOffset = CapsuleColliderInstance.contactOffset;
        hideFlags = CapsuleColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class BoxColliderZSaver : ZSaver.ZSaver<UnityEngine.BoxCollider> {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public BoxColliderZSaver (UnityEngine.BoxCollider BoxColliderInstance) : base(BoxColliderInstance.gameObject, BoxColliderInstance ) {
        center = BoxColliderInstance.center;
        size = BoxColliderInstance.size;
        enabled = BoxColliderInstance.enabled;
        isTrigger = BoxColliderInstance.isTrigger;
        contactOffset = BoxColliderInstance.contactOffset;
        hideFlags = BoxColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class SphereColliderZSaver : ZSaver.ZSaver<UnityEngine.SphereCollider> {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public SphereColliderZSaver (UnityEngine.SphereCollider SphereColliderInstance) : base(SphereColliderInstance.gameObject, SphereColliderInstance ) {
        center = SphereColliderInstance.center;
        radius = SphereColliderInstance.radius;
        enabled = SphereColliderInstance.enabled;
        isTrigger = SphereColliderInstance.isTrigger;
        contactOffset = SphereColliderInstance.contactOffset;
        hideFlags = SphereColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class ConstantForceZSaver : ZSaver.ZSaver<UnityEngine.ConstantForce> {
    public UnityEngine.Vector3 force;
    public UnityEngine.Vector3 relativeForce;
    public UnityEngine.Vector3 torque;
    public UnityEngine.Vector3 relativeTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ConstantForceZSaver (UnityEngine.ConstantForce ConstantForceInstance) : base(ConstantForceInstance.gameObject, ConstantForceInstance ) {
        force = ConstantForceInstance.force;
        relativeForce = ConstantForceInstance.relativeForce;
        torque = ConstantForceInstance.torque;
        relativeTorque = ConstantForceInstance.relativeTorque;
        enabled = ConstantForceInstance.enabled;
        hideFlags = ConstantForceInstance.hideFlags;

    }
}
[System.Serializable]
public class JointZSaver : ZSaver.ZSaver<UnityEngine.Joint> {
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
    public UnityEngine.HideFlags hideFlags;
    public JointZSaver (UnityEngine.Joint JointInstance) : base(JointInstance.gameObject, JointInstance ) {
        connectedBody = JointInstance.connectedBody;
        connectedArticulationBody = JointInstance.connectedArticulationBody;
        axis = JointInstance.axis;
        anchor = JointInstance.anchor;
        connectedAnchor = JointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = JointInstance.autoConfigureConnectedAnchor;
        breakForce = JointInstance.breakForce;
        breakTorque = JointInstance.breakTorque;
        enableCollision = JointInstance.enableCollision;
        enablePreprocessing = JointInstance.enablePreprocessing;
        massScale = JointInstance.massScale;
        connectedMassScale = JointInstance.connectedMassScale;
        hideFlags = JointInstance.hideFlags;

    }
}
[System.Serializable]
public class HingeJointZSaver : ZSaver.ZSaver<UnityEngine.HingeJoint> {
    public UnityEngine.JointMotor motor;
    public UnityEngine.JointLimits limits;
    public UnityEngine.JointSpring spring;
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public System.Boolean useSpring;
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
    public UnityEngine.HideFlags hideFlags;
    public HingeJointZSaver (UnityEngine.HingeJoint HingeJointInstance) : base(HingeJointInstance.gameObject, HingeJointInstance ) {
        motor = HingeJointInstance.motor;
        limits = HingeJointInstance.limits;
        spring = HingeJointInstance.spring;
        useMotor = HingeJointInstance.useMotor;
        useLimits = HingeJointInstance.useLimits;
        useSpring = HingeJointInstance.useSpring;
        connectedBody = HingeJointInstance.connectedBody;
        connectedArticulationBody = HingeJointInstance.connectedArticulationBody;
        axis = HingeJointInstance.axis;
        anchor = HingeJointInstance.anchor;
        connectedAnchor = HingeJointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = HingeJointInstance.autoConfigureConnectedAnchor;
        breakForce = HingeJointInstance.breakForce;
        breakTorque = HingeJointInstance.breakTorque;
        enableCollision = HingeJointInstance.enableCollision;
        enablePreprocessing = HingeJointInstance.enablePreprocessing;
        massScale = HingeJointInstance.massScale;
        connectedMassScale = HingeJointInstance.connectedMassScale;
        hideFlags = HingeJointInstance.hideFlags;

    }
}
[System.Serializable]
public class SpringJointZSaver : ZSaver.ZSaver<UnityEngine.SpringJoint> {
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
    public UnityEngine.HideFlags hideFlags;
    public SpringJointZSaver (UnityEngine.SpringJoint SpringJointInstance) : base(SpringJointInstance.gameObject, SpringJointInstance ) {
        spring = SpringJointInstance.spring;
        damper = SpringJointInstance.damper;
        minDistance = SpringJointInstance.minDistance;
        maxDistance = SpringJointInstance.maxDistance;
        tolerance = SpringJointInstance.tolerance;
        connectedBody = SpringJointInstance.connectedBody;
        connectedArticulationBody = SpringJointInstance.connectedArticulationBody;
        axis = SpringJointInstance.axis;
        anchor = SpringJointInstance.anchor;
        connectedAnchor = SpringJointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = SpringJointInstance.autoConfigureConnectedAnchor;
        breakForce = SpringJointInstance.breakForce;
        breakTorque = SpringJointInstance.breakTorque;
        enableCollision = SpringJointInstance.enableCollision;
        enablePreprocessing = SpringJointInstance.enablePreprocessing;
        massScale = SpringJointInstance.massScale;
        connectedMassScale = SpringJointInstance.connectedMassScale;
        hideFlags = SpringJointInstance.hideFlags;

    }
}
[System.Serializable]
public class FixedJointZSaver : ZSaver.ZSaver<UnityEngine.FixedJoint> {
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
    public UnityEngine.HideFlags hideFlags;
    public FixedJointZSaver (UnityEngine.FixedJoint FixedJointInstance) : base(FixedJointInstance.gameObject, FixedJointInstance ) {
        connectedBody = FixedJointInstance.connectedBody;
        connectedArticulationBody = FixedJointInstance.connectedArticulationBody;
        axis = FixedJointInstance.axis;
        anchor = FixedJointInstance.anchor;
        connectedAnchor = FixedJointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = FixedJointInstance.autoConfigureConnectedAnchor;
        breakForce = FixedJointInstance.breakForce;
        breakTorque = FixedJointInstance.breakTorque;
        enableCollision = FixedJointInstance.enableCollision;
        enablePreprocessing = FixedJointInstance.enablePreprocessing;
        massScale = FixedJointInstance.massScale;
        connectedMassScale = FixedJointInstance.connectedMassScale;
        hideFlags = FixedJointInstance.hideFlags;

    }
}
[System.Serializable]
public class CharacterJointZSaver : ZSaver.ZSaver<UnityEngine.CharacterJoint> {
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
    public UnityEngine.HideFlags hideFlags;
    public CharacterJointZSaver (UnityEngine.CharacterJoint CharacterJointInstance) : base(CharacterJointInstance.gameObject, CharacterJointInstance ) {
        swingAxis = CharacterJointInstance.swingAxis;
        twistLimitSpring = CharacterJointInstance.twistLimitSpring;
        swingLimitSpring = CharacterJointInstance.swingLimitSpring;
        lowTwistLimit = CharacterJointInstance.lowTwistLimit;
        highTwistLimit = CharacterJointInstance.highTwistLimit;
        swing1Limit = CharacterJointInstance.swing1Limit;
        swing2Limit = CharacterJointInstance.swing2Limit;
        enableProjection = CharacterJointInstance.enableProjection;
        projectionDistance = CharacterJointInstance.projectionDistance;
        projectionAngle = CharacterJointInstance.projectionAngle;
        connectedBody = CharacterJointInstance.connectedBody;
        connectedArticulationBody = CharacterJointInstance.connectedArticulationBody;
        axis = CharacterJointInstance.axis;
        anchor = CharacterJointInstance.anchor;
        connectedAnchor = CharacterJointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = CharacterJointInstance.autoConfigureConnectedAnchor;
        breakForce = CharacterJointInstance.breakForce;
        breakTorque = CharacterJointInstance.breakTorque;
        enableCollision = CharacterJointInstance.enableCollision;
        enablePreprocessing = CharacterJointInstance.enablePreprocessing;
        massScale = CharacterJointInstance.massScale;
        connectedMassScale = CharacterJointInstance.connectedMassScale;
        hideFlags = CharacterJointInstance.hideFlags;

    }
}
[System.Serializable]
public class ConfigurableJointZSaver : ZSaver.ZSaver<UnityEngine.ConfigurableJoint> {
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
    public UnityEngine.HideFlags hideFlags;
    public ConfigurableJointZSaver (UnityEngine.ConfigurableJoint ConfigurableJointInstance) : base(ConfigurableJointInstance.gameObject, ConfigurableJointInstance ) {
        secondaryAxis = ConfigurableJointInstance.secondaryAxis;
        xMotion = ConfigurableJointInstance.xMotion;
        yMotion = ConfigurableJointInstance.yMotion;
        zMotion = ConfigurableJointInstance.zMotion;
        angularXMotion = ConfigurableJointInstance.angularXMotion;
        angularYMotion = ConfigurableJointInstance.angularYMotion;
        angularZMotion = ConfigurableJointInstance.angularZMotion;
        linearLimitSpring = ConfigurableJointInstance.linearLimitSpring;
        angularXLimitSpring = ConfigurableJointInstance.angularXLimitSpring;
        angularYZLimitSpring = ConfigurableJointInstance.angularYZLimitSpring;
        linearLimit = ConfigurableJointInstance.linearLimit;
        lowAngularXLimit = ConfigurableJointInstance.lowAngularXLimit;
        highAngularXLimit = ConfigurableJointInstance.highAngularXLimit;
        angularYLimit = ConfigurableJointInstance.angularYLimit;
        angularZLimit = ConfigurableJointInstance.angularZLimit;
        targetPosition = ConfigurableJointInstance.targetPosition;
        targetVelocity = ConfigurableJointInstance.targetVelocity;
        xDrive = ConfigurableJointInstance.xDrive;
        yDrive = ConfigurableJointInstance.yDrive;
        zDrive = ConfigurableJointInstance.zDrive;
        targetRotation = ConfigurableJointInstance.targetRotation;
        targetAngularVelocity = ConfigurableJointInstance.targetAngularVelocity;
        rotationDriveMode = ConfigurableJointInstance.rotationDriveMode;
        angularXDrive = ConfigurableJointInstance.angularXDrive;
        angularYZDrive = ConfigurableJointInstance.angularYZDrive;
        slerpDrive = ConfigurableJointInstance.slerpDrive;
        projectionMode = ConfigurableJointInstance.projectionMode;
        projectionDistance = ConfigurableJointInstance.projectionDistance;
        projectionAngle = ConfigurableJointInstance.projectionAngle;
        configuredInWorldSpace = ConfigurableJointInstance.configuredInWorldSpace;
        swapBodies = ConfigurableJointInstance.swapBodies;
        connectedBody = ConfigurableJointInstance.connectedBody;
        connectedArticulationBody = ConfigurableJointInstance.connectedArticulationBody;
        axis = ConfigurableJointInstance.axis;
        anchor = ConfigurableJointInstance.anchor;
        connectedAnchor = ConfigurableJointInstance.connectedAnchor;
        autoConfigureConnectedAnchor = ConfigurableJointInstance.autoConfigureConnectedAnchor;
        breakForce = ConfigurableJointInstance.breakForce;
        breakTorque = ConfigurableJointInstance.breakTorque;
        enableCollision = ConfigurableJointInstance.enableCollision;
        enablePreprocessing = ConfigurableJointInstance.enablePreprocessing;
        massScale = ConfigurableJointInstance.massScale;
        connectedMassScale = ConfigurableJointInstance.connectedMassScale;
        hideFlags = ConfigurableJointInstance.hideFlags;

    }
}
[System.Serializable]
public class ArticulationBodyZSaver : ZSaver.ZSaver<UnityEngine.ArticulationBody> {
    public UnityEngine.ArticulationJointType jointType;
    public UnityEngine.Vector3 anchorPosition;
    public UnityEngine.Vector3 parentAnchorPosition;
    public UnityEngine.Quaternion anchorRotation;
    public UnityEngine.Quaternion parentAnchorRotation;
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
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ArticulationBodyZSaver (UnityEngine.ArticulationBody ArticulationBodyInstance) : base(ArticulationBodyInstance.gameObject, ArticulationBodyInstance ) {
        jointType = ArticulationBodyInstance.jointType;
        anchorPosition = ArticulationBodyInstance.anchorPosition;
        parentAnchorPosition = ArticulationBodyInstance.parentAnchorPosition;
        anchorRotation = ArticulationBodyInstance.anchorRotation;
        parentAnchorRotation = ArticulationBodyInstance.parentAnchorRotation;
        linearLockX = ArticulationBodyInstance.linearLockX;
        linearLockY = ArticulationBodyInstance.linearLockY;
        linearLockZ = ArticulationBodyInstance.linearLockZ;
        swingYLock = ArticulationBodyInstance.swingYLock;
        swingZLock = ArticulationBodyInstance.swingZLock;
        twistLock = ArticulationBodyInstance.twistLock;
        xDrive = ArticulationBodyInstance.xDrive;
        yDrive = ArticulationBodyInstance.yDrive;
        zDrive = ArticulationBodyInstance.zDrive;
        immovable = ArticulationBodyInstance.immovable;
        useGravity = ArticulationBodyInstance.useGravity;
        linearDamping = ArticulationBodyInstance.linearDamping;
        angularDamping = ArticulationBodyInstance.angularDamping;
        jointFriction = ArticulationBodyInstance.jointFriction;
        velocity = ArticulationBodyInstance.velocity;
        angularVelocity = ArticulationBodyInstance.angularVelocity;
        mass = ArticulationBodyInstance.mass;
        centerOfMass = ArticulationBodyInstance.centerOfMass;
        inertiaTensor = ArticulationBodyInstance.inertiaTensor;
        inertiaTensorRotation = ArticulationBodyInstance.inertiaTensorRotation;
        sleepThreshold = ArticulationBodyInstance.sleepThreshold;
        solverIterations = ArticulationBodyInstance.solverIterations;
        solverVelocityIterations = ArticulationBodyInstance.solverVelocityIterations;
        maxAngularVelocity = ArticulationBodyInstance.maxAngularVelocity;
        maxLinearVelocity = ArticulationBodyInstance.maxLinearVelocity;
        maxJointVelocity = ArticulationBodyInstance.maxJointVelocity;
        maxDepenetrationVelocity = ArticulationBodyInstance.maxDepenetrationVelocity;
        jointPosition = ArticulationBodyInstance.jointPosition;
        jointVelocity = ArticulationBodyInstance.jointVelocity;
        jointAcceleration = ArticulationBodyInstance.jointAcceleration;
        jointForce = ArticulationBodyInstance.jointForce;
        enabled = ArticulationBodyInstance.enabled;
        hideFlags = ArticulationBodyInstance.hideFlags;

    }
}
[System.Serializable]
public class Rigidbody2DZSaver : ZSaver.ZSaver<UnityEngine.Rigidbody2D> {
    public UnityEngine.Vector2 position;
    public System.Single rotation;
    public UnityEngine.Vector2 velocity;
    public System.Single angularVelocity;
    public System.Boolean useAutoMass;
    public System.Single mass;
    public UnityEngine.Vector2 centerOfMass;
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
    public UnityEngine.HideFlags hideFlags;
    public Rigidbody2DZSaver (UnityEngine.Rigidbody2D Rigidbody2DInstance) : base(Rigidbody2DInstance.gameObject, Rigidbody2DInstance ) {
        position = Rigidbody2DInstance.position;
        rotation = Rigidbody2DInstance.rotation;
        velocity = Rigidbody2DInstance.velocity;
        angularVelocity = Rigidbody2DInstance.angularVelocity;
        useAutoMass = Rigidbody2DInstance.useAutoMass;
        mass = Rigidbody2DInstance.mass;
        centerOfMass = Rigidbody2DInstance.centerOfMass;
        inertia = Rigidbody2DInstance.inertia;
        drag = Rigidbody2DInstance.drag;
        angularDrag = Rigidbody2DInstance.angularDrag;
        gravityScale = Rigidbody2DInstance.gravityScale;
        bodyType = Rigidbody2DInstance.bodyType;
        useFullKinematicContacts = Rigidbody2DInstance.useFullKinematicContacts;
        isKinematic = Rigidbody2DInstance.isKinematic;
        freezeRotation = Rigidbody2DInstance.freezeRotation;
        constraints = Rigidbody2DInstance.constraints;
        simulated = Rigidbody2DInstance.simulated;
        interpolation = Rigidbody2DInstance.interpolation;
        sleepMode = Rigidbody2DInstance.sleepMode;
        collisionDetectionMode = Rigidbody2DInstance.collisionDetectionMode;
        hideFlags = Rigidbody2DInstance.hideFlags;

    }
}
[System.Serializable]
public class Collider2DZSaver : ZSaver.ZSaver<UnityEngine.Collider2D> {
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public Collider2DZSaver (UnityEngine.Collider2D Collider2DInstance) : base(Collider2DInstance.gameObject, Collider2DInstance ) {
        density = Collider2DInstance.density;
        isTrigger = Collider2DInstance.isTrigger;
        usedByEffector = Collider2DInstance.usedByEffector;
        usedByComposite = Collider2DInstance.usedByComposite;
        offset = Collider2DInstance.offset;
        enabled = Collider2DInstance.enabled;
        hideFlags = Collider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class CircleCollider2DZSaver : ZSaver.ZSaver<UnityEngine.CircleCollider2D> {
    public System.Single radius;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CircleCollider2DZSaver (UnityEngine.CircleCollider2D CircleCollider2DInstance) : base(CircleCollider2DInstance.gameObject, CircleCollider2DInstance ) {
        radius = CircleCollider2DInstance.radius;
        density = CircleCollider2DInstance.density;
        isTrigger = CircleCollider2DInstance.isTrigger;
        usedByEffector = CircleCollider2DInstance.usedByEffector;
        usedByComposite = CircleCollider2DInstance.usedByComposite;
        offset = CircleCollider2DInstance.offset;
        enabled = CircleCollider2DInstance.enabled;
        hideFlags = CircleCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class CapsuleCollider2DZSaver : ZSaver.ZSaver<UnityEngine.CapsuleCollider2D> {
    public UnityEngine.Vector2 size;
    public UnityEngine.CapsuleDirection2D direction;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CapsuleCollider2DZSaver (UnityEngine.CapsuleCollider2D CapsuleCollider2DInstance) : base(CapsuleCollider2DInstance.gameObject, CapsuleCollider2DInstance ) {
        size = CapsuleCollider2DInstance.size;
        direction = CapsuleCollider2DInstance.direction;
        density = CapsuleCollider2DInstance.density;
        isTrigger = CapsuleCollider2DInstance.isTrigger;
        usedByEffector = CapsuleCollider2DInstance.usedByEffector;
        usedByComposite = CapsuleCollider2DInstance.usedByComposite;
        offset = CapsuleCollider2DInstance.offset;
        enabled = CapsuleCollider2DInstance.enabled;
        hideFlags = CapsuleCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class EdgeCollider2DZSaver : ZSaver.ZSaver<UnityEngine.EdgeCollider2D> {
    public System.Single edgeRadius;
    public UnityEngine.Vector2[] points;
    public System.Boolean useAdjacentStartPoint;
    public System.Boolean useAdjacentEndPoint;
    public UnityEngine.Vector2 adjacentStartPoint;
    public UnityEngine.Vector2 adjacentEndPoint;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public EdgeCollider2DZSaver (UnityEngine.EdgeCollider2D EdgeCollider2DInstance) : base(EdgeCollider2DInstance.gameObject, EdgeCollider2DInstance ) {
        edgeRadius = EdgeCollider2DInstance.edgeRadius;
        points = EdgeCollider2DInstance.points;
        useAdjacentStartPoint = EdgeCollider2DInstance.useAdjacentStartPoint;
        useAdjacentEndPoint = EdgeCollider2DInstance.useAdjacentEndPoint;
        adjacentStartPoint = EdgeCollider2DInstance.adjacentStartPoint;
        adjacentEndPoint = EdgeCollider2DInstance.adjacentEndPoint;
        density = EdgeCollider2DInstance.density;
        isTrigger = EdgeCollider2DInstance.isTrigger;
        usedByEffector = EdgeCollider2DInstance.usedByEffector;
        usedByComposite = EdgeCollider2DInstance.usedByComposite;
        offset = EdgeCollider2DInstance.offset;
        enabled = EdgeCollider2DInstance.enabled;
        hideFlags = EdgeCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class BoxCollider2DZSaver : ZSaver.ZSaver<UnityEngine.BoxCollider2D> {
    public UnityEngine.Vector2 size;
    public System.Single edgeRadius;
    public System.Boolean autoTiling;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public BoxCollider2DZSaver (UnityEngine.BoxCollider2D BoxCollider2DInstance) : base(BoxCollider2DInstance.gameObject, BoxCollider2DInstance ) {
        size = BoxCollider2DInstance.size;
        edgeRadius = BoxCollider2DInstance.edgeRadius;
        autoTiling = BoxCollider2DInstance.autoTiling;
        density = BoxCollider2DInstance.density;
        isTrigger = BoxCollider2DInstance.isTrigger;
        usedByEffector = BoxCollider2DInstance.usedByEffector;
        usedByComposite = BoxCollider2DInstance.usedByComposite;
        offset = BoxCollider2DInstance.offset;
        enabled = BoxCollider2DInstance.enabled;
        hideFlags = BoxCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class PolygonCollider2DZSaver : ZSaver.ZSaver<UnityEngine.PolygonCollider2D> {
    public System.Boolean autoTiling;
    public UnityEngine.Vector2[] points;
    public System.Int32 pathCount;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public PolygonCollider2DZSaver (UnityEngine.PolygonCollider2D PolygonCollider2DInstance) : base(PolygonCollider2DInstance.gameObject, PolygonCollider2DInstance ) {
        autoTiling = PolygonCollider2DInstance.autoTiling;
        points = PolygonCollider2DInstance.points;
        pathCount = PolygonCollider2DInstance.pathCount;
        density = PolygonCollider2DInstance.density;
        isTrigger = PolygonCollider2DInstance.isTrigger;
        usedByEffector = PolygonCollider2DInstance.usedByEffector;
        usedByComposite = PolygonCollider2DInstance.usedByComposite;
        offset = PolygonCollider2DInstance.offset;
        enabled = PolygonCollider2DInstance.enabled;
        hideFlags = PolygonCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class CompositeCollider2DZSaver : ZSaver.ZSaver<UnityEngine.CompositeCollider2D> {
    public UnityEngine.CompositeCollider2D.GeometryType geometryType;
    public UnityEngine.CompositeCollider2D.GenerationType generationType;
    public System.Single vertexDistance;
    public System.Single edgeRadius;
    public System.Single offsetDistance;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CompositeCollider2DZSaver (UnityEngine.CompositeCollider2D CompositeCollider2DInstance) : base(CompositeCollider2DInstance.gameObject, CompositeCollider2DInstance ) {
        geometryType = CompositeCollider2DInstance.geometryType;
        generationType = CompositeCollider2DInstance.generationType;
        vertexDistance = CompositeCollider2DInstance.vertexDistance;
        edgeRadius = CompositeCollider2DInstance.edgeRadius;
        offsetDistance = CompositeCollider2DInstance.offsetDistance;
        density = CompositeCollider2DInstance.density;
        isTrigger = CompositeCollider2DInstance.isTrigger;
        usedByEffector = CompositeCollider2DInstance.usedByEffector;
        usedByComposite = CompositeCollider2DInstance.usedByComposite;
        offset = CompositeCollider2DInstance.offset;
        enabled = CompositeCollider2DInstance.enabled;
        hideFlags = CompositeCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class Joint2DZSaver : ZSaver.ZSaver<UnityEngine.Joint2D> {
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public Joint2DZSaver (UnityEngine.Joint2D Joint2DInstance) : base(Joint2DInstance.gameObject, Joint2DInstance ) {
        connectedBody = Joint2DInstance.connectedBody;
        enableCollision = Joint2DInstance.enableCollision;
        breakForce = Joint2DInstance.breakForce;
        breakTorque = Joint2DInstance.breakTorque;
        enabled = Joint2DInstance.enabled;
        hideFlags = Joint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class AnchoredJoint2DZSaver : ZSaver.ZSaver<UnityEngine.AnchoredJoint2D> {
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AnchoredJoint2DZSaver (UnityEngine.AnchoredJoint2D AnchoredJoint2DInstance) : base(AnchoredJoint2DInstance.gameObject, AnchoredJoint2DInstance ) {
        anchor = AnchoredJoint2DInstance.anchor;
        connectedAnchor = AnchoredJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = AnchoredJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = AnchoredJoint2DInstance.connectedBody;
        enableCollision = AnchoredJoint2DInstance.enableCollision;
        breakForce = AnchoredJoint2DInstance.breakForce;
        breakTorque = AnchoredJoint2DInstance.breakTorque;
        enabled = AnchoredJoint2DInstance.enabled;
        hideFlags = AnchoredJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class SpringJoint2DZSaver : ZSaver.ZSaver<UnityEngine.SpringJoint2D> {
    public System.Boolean autoConfigureDistance;
    public System.Single distance;
    public System.Single dampingRatio;
    public System.Single frequency;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public SpringJoint2DZSaver (UnityEngine.SpringJoint2D SpringJoint2DInstance) : base(SpringJoint2DInstance.gameObject, SpringJoint2DInstance ) {
        autoConfigureDistance = SpringJoint2DInstance.autoConfigureDistance;
        distance = SpringJoint2DInstance.distance;
        dampingRatio = SpringJoint2DInstance.dampingRatio;
        frequency = SpringJoint2DInstance.frequency;
        anchor = SpringJoint2DInstance.anchor;
        connectedAnchor = SpringJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = SpringJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = SpringJoint2DInstance.connectedBody;
        enableCollision = SpringJoint2DInstance.enableCollision;
        breakForce = SpringJoint2DInstance.breakForce;
        breakTorque = SpringJoint2DInstance.breakTorque;
        enabled = SpringJoint2DInstance.enabled;
        hideFlags = SpringJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class DistanceJoint2DZSaver : ZSaver.ZSaver<UnityEngine.DistanceJoint2D> {
    public System.Boolean autoConfigureDistance;
    public System.Single distance;
    public System.Boolean maxDistanceOnly;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public DistanceJoint2DZSaver (UnityEngine.DistanceJoint2D DistanceJoint2DInstance) : base(DistanceJoint2DInstance.gameObject, DistanceJoint2DInstance ) {
        autoConfigureDistance = DistanceJoint2DInstance.autoConfigureDistance;
        distance = DistanceJoint2DInstance.distance;
        maxDistanceOnly = DistanceJoint2DInstance.maxDistanceOnly;
        anchor = DistanceJoint2DInstance.anchor;
        connectedAnchor = DistanceJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = DistanceJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = DistanceJoint2DInstance.connectedBody;
        enableCollision = DistanceJoint2DInstance.enableCollision;
        breakForce = DistanceJoint2DInstance.breakForce;
        breakTorque = DistanceJoint2DInstance.breakTorque;
        enabled = DistanceJoint2DInstance.enabled;
        hideFlags = DistanceJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class FrictionJoint2DZSaver : ZSaver.ZSaver<UnityEngine.FrictionJoint2D> {
    public System.Single maxForce;
    public System.Single maxTorque;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public FrictionJoint2DZSaver (UnityEngine.FrictionJoint2D FrictionJoint2DInstance) : base(FrictionJoint2DInstance.gameObject, FrictionJoint2DInstance ) {
        maxForce = FrictionJoint2DInstance.maxForce;
        maxTorque = FrictionJoint2DInstance.maxTorque;
        anchor = FrictionJoint2DInstance.anchor;
        connectedAnchor = FrictionJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = FrictionJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = FrictionJoint2DInstance.connectedBody;
        enableCollision = FrictionJoint2DInstance.enableCollision;
        breakForce = FrictionJoint2DInstance.breakForce;
        breakTorque = FrictionJoint2DInstance.breakTorque;
        enabled = FrictionJoint2DInstance.enabled;
        hideFlags = FrictionJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class HingeJoint2DZSaver : ZSaver.ZSaver<UnityEngine.HingeJoint2D> {
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public UnityEngine.JointMotor2D motor;
    public UnityEngine.JointAngleLimits2D limits;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public HingeJoint2DZSaver (UnityEngine.HingeJoint2D HingeJoint2DInstance) : base(HingeJoint2DInstance.gameObject, HingeJoint2DInstance ) {
        useMotor = HingeJoint2DInstance.useMotor;
        useLimits = HingeJoint2DInstance.useLimits;
        motor = HingeJoint2DInstance.motor;
        limits = HingeJoint2DInstance.limits;
        anchor = HingeJoint2DInstance.anchor;
        connectedAnchor = HingeJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = HingeJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = HingeJoint2DInstance.connectedBody;
        enableCollision = HingeJoint2DInstance.enableCollision;
        breakForce = HingeJoint2DInstance.breakForce;
        breakTorque = HingeJoint2DInstance.breakTorque;
        enabled = HingeJoint2DInstance.enabled;
        hideFlags = HingeJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class RelativeJoint2DZSaver : ZSaver.ZSaver<UnityEngine.RelativeJoint2D> {
    public System.Single maxForce;
    public System.Single maxTorque;
    public System.Single correctionScale;
    public System.Boolean autoConfigureOffset;
    public UnityEngine.Vector2 linearOffset;
    public System.Single angularOffset;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public RelativeJoint2DZSaver (UnityEngine.RelativeJoint2D RelativeJoint2DInstance) : base(RelativeJoint2DInstance.gameObject, RelativeJoint2DInstance ) {
        maxForce = RelativeJoint2DInstance.maxForce;
        maxTorque = RelativeJoint2DInstance.maxTorque;
        correctionScale = RelativeJoint2DInstance.correctionScale;
        autoConfigureOffset = RelativeJoint2DInstance.autoConfigureOffset;
        linearOffset = RelativeJoint2DInstance.linearOffset;
        angularOffset = RelativeJoint2DInstance.angularOffset;
        connectedBody = RelativeJoint2DInstance.connectedBody;
        enableCollision = RelativeJoint2DInstance.enableCollision;
        breakForce = RelativeJoint2DInstance.breakForce;
        breakTorque = RelativeJoint2DInstance.breakTorque;
        enabled = RelativeJoint2DInstance.enabled;
        hideFlags = RelativeJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class SliderJoint2DZSaver : ZSaver.ZSaver<UnityEngine.SliderJoint2D> {
    public System.Boolean autoConfigureAngle;
    public System.Single angle;
    public System.Boolean useMotor;
    public System.Boolean useLimits;
    public UnityEngine.JointMotor2D motor;
    public UnityEngine.JointTranslationLimits2D limits;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public SliderJoint2DZSaver (UnityEngine.SliderJoint2D SliderJoint2DInstance) : base(SliderJoint2DInstance.gameObject, SliderJoint2DInstance ) {
        autoConfigureAngle = SliderJoint2DInstance.autoConfigureAngle;
        angle = SliderJoint2DInstance.angle;
        useMotor = SliderJoint2DInstance.useMotor;
        useLimits = SliderJoint2DInstance.useLimits;
        motor = SliderJoint2DInstance.motor;
        limits = SliderJoint2DInstance.limits;
        anchor = SliderJoint2DInstance.anchor;
        connectedAnchor = SliderJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = SliderJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = SliderJoint2DInstance.connectedBody;
        enableCollision = SliderJoint2DInstance.enableCollision;
        breakForce = SliderJoint2DInstance.breakForce;
        breakTorque = SliderJoint2DInstance.breakTorque;
        enabled = SliderJoint2DInstance.enabled;
        hideFlags = SliderJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class TargetJoint2DZSaver : ZSaver.ZSaver<UnityEngine.TargetJoint2D> {
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 target;
    public System.Boolean autoConfigureTarget;
    public System.Single maxForce;
    public System.Single dampingRatio;
    public System.Single frequency;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public TargetJoint2DZSaver (UnityEngine.TargetJoint2D TargetJoint2DInstance) : base(TargetJoint2DInstance.gameObject, TargetJoint2DInstance ) {
        anchor = TargetJoint2DInstance.anchor;
        target = TargetJoint2DInstance.target;
        autoConfigureTarget = TargetJoint2DInstance.autoConfigureTarget;
        maxForce = TargetJoint2DInstance.maxForce;
        dampingRatio = TargetJoint2DInstance.dampingRatio;
        frequency = TargetJoint2DInstance.frequency;
        connectedBody = TargetJoint2DInstance.connectedBody;
        enableCollision = TargetJoint2DInstance.enableCollision;
        breakForce = TargetJoint2DInstance.breakForce;
        breakTorque = TargetJoint2DInstance.breakTorque;
        enabled = TargetJoint2DInstance.enabled;
        hideFlags = TargetJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class FixedJoint2DZSaver : ZSaver.ZSaver<UnityEngine.FixedJoint2D> {
    public System.Single dampingRatio;
    public System.Single frequency;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public FixedJoint2DZSaver (UnityEngine.FixedJoint2D FixedJoint2DInstance) : base(FixedJoint2DInstance.gameObject, FixedJoint2DInstance ) {
        dampingRatio = FixedJoint2DInstance.dampingRatio;
        frequency = FixedJoint2DInstance.frequency;
        anchor = FixedJoint2DInstance.anchor;
        connectedAnchor = FixedJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = FixedJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = FixedJoint2DInstance.connectedBody;
        enableCollision = FixedJoint2DInstance.enableCollision;
        breakForce = FixedJoint2DInstance.breakForce;
        breakTorque = FixedJoint2DInstance.breakTorque;
        enabled = FixedJoint2DInstance.enabled;
        hideFlags = FixedJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class WheelJoint2DZSaver : ZSaver.ZSaver<UnityEngine.WheelJoint2D> {
    public UnityEngine.JointSuspension2D suspension;
    public System.Boolean useMotor;
    public UnityEngine.JointMotor2D motor;
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public WheelJoint2DZSaver (UnityEngine.WheelJoint2D WheelJoint2DInstance) : base(WheelJoint2DInstance.gameObject, WheelJoint2DInstance ) {
        suspension = WheelJoint2DInstance.suspension;
        useMotor = WheelJoint2DInstance.useMotor;
        motor = WheelJoint2DInstance.motor;
        anchor = WheelJoint2DInstance.anchor;
        connectedAnchor = WheelJoint2DInstance.connectedAnchor;
        autoConfigureConnectedAnchor = WheelJoint2DInstance.autoConfigureConnectedAnchor;
        connectedBody = WheelJoint2DInstance.connectedBody;
        enableCollision = WheelJoint2DInstance.enableCollision;
        breakForce = WheelJoint2DInstance.breakForce;
        breakTorque = WheelJoint2DInstance.breakTorque;
        enabled = WheelJoint2DInstance.enabled;
        hideFlags = WheelJoint2DInstance.hideFlags;

    }
}
[System.Serializable]
public class Effector2DZSaver : ZSaver.ZSaver<UnityEngine.Effector2D> {
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public Effector2DZSaver (UnityEngine.Effector2D Effector2DInstance) : base(Effector2DInstance.gameObject, Effector2DInstance ) {
        useColliderMask = Effector2DInstance.useColliderMask;
        colliderMask = Effector2DInstance.colliderMask;
        enabled = Effector2DInstance.enabled;
        hideFlags = Effector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class AreaEffector2DZSaver : ZSaver.ZSaver<UnityEngine.AreaEffector2D> {
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
    public UnityEngine.HideFlags hideFlags;
    public AreaEffector2DZSaver (UnityEngine.AreaEffector2D AreaEffector2DInstance) : base(AreaEffector2DInstance.gameObject, AreaEffector2DInstance ) {
        forceAngle = AreaEffector2DInstance.forceAngle;
        useGlobalAngle = AreaEffector2DInstance.useGlobalAngle;
        forceMagnitude = AreaEffector2DInstance.forceMagnitude;
        forceVariation = AreaEffector2DInstance.forceVariation;
        drag = AreaEffector2DInstance.drag;
        angularDrag = AreaEffector2DInstance.angularDrag;
        forceTarget = AreaEffector2DInstance.forceTarget;
        useColliderMask = AreaEffector2DInstance.useColliderMask;
        colliderMask = AreaEffector2DInstance.colliderMask;
        enabled = AreaEffector2DInstance.enabled;
        hideFlags = AreaEffector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class BuoyancyEffector2DZSaver : ZSaver.ZSaver<UnityEngine.BuoyancyEffector2D> {
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
    public UnityEngine.HideFlags hideFlags;
    public BuoyancyEffector2DZSaver (UnityEngine.BuoyancyEffector2D BuoyancyEffector2DInstance) : base(BuoyancyEffector2DInstance.gameObject, BuoyancyEffector2DInstance ) {
        surfaceLevel = BuoyancyEffector2DInstance.surfaceLevel;
        density = BuoyancyEffector2DInstance.density;
        linearDrag = BuoyancyEffector2DInstance.linearDrag;
        angularDrag = BuoyancyEffector2DInstance.angularDrag;
        flowAngle = BuoyancyEffector2DInstance.flowAngle;
        flowMagnitude = BuoyancyEffector2DInstance.flowMagnitude;
        flowVariation = BuoyancyEffector2DInstance.flowVariation;
        useColliderMask = BuoyancyEffector2DInstance.useColliderMask;
        colliderMask = BuoyancyEffector2DInstance.colliderMask;
        enabled = BuoyancyEffector2DInstance.enabled;
        hideFlags = BuoyancyEffector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class PointEffector2DZSaver : ZSaver.ZSaver<UnityEngine.PointEffector2D> {
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
    public UnityEngine.HideFlags hideFlags;
    public PointEffector2DZSaver (UnityEngine.PointEffector2D PointEffector2DInstance) : base(PointEffector2DInstance.gameObject, PointEffector2DInstance ) {
        forceMagnitude = PointEffector2DInstance.forceMagnitude;
        forceVariation = PointEffector2DInstance.forceVariation;
        distanceScale = PointEffector2DInstance.distanceScale;
        drag = PointEffector2DInstance.drag;
        angularDrag = PointEffector2DInstance.angularDrag;
        forceSource = PointEffector2DInstance.forceSource;
        forceTarget = PointEffector2DInstance.forceTarget;
        forceMode = PointEffector2DInstance.forceMode;
        useColliderMask = PointEffector2DInstance.useColliderMask;
        colliderMask = PointEffector2DInstance.colliderMask;
        enabled = PointEffector2DInstance.enabled;
        hideFlags = PointEffector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class PlatformEffector2DZSaver : ZSaver.ZSaver<UnityEngine.PlatformEffector2D> {
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
    public UnityEngine.HideFlags hideFlags;
    public PlatformEffector2DZSaver (UnityEngine.PlatformEffector2D PlatformEffector2DInstance) : base(PlatformEffector2DInstance.gameObject, PlatformEffector2DInstance ) {
        useOneWay = PlatformEffector2DInstance.useOneWay;
        useOneWayGrouping = PlatformEffector2DInstance.useOneWayGrouping;
        useSideFriction = PlatformEffector2DInstance.useSideFriction;
        useSideBounce = PlatformEffector2DInstance.useSideBounce;
        surfaceArc = PlatformEffector2DInstance.surfaceArc;
        sideArc = PlatformEffector2DInstance.sideArc;
        rotationalOffset = PlatformEffector2DInstance.rotationalOffset;
        useColliderMask = PlatformEffector2DInstance.useColliderMask;
        colliderMask = PlatformEffector2DInstance.colliderMask;
        enabled = PlatformEffector2DInstance.enabled;
        hideFlags = PlatformEffector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class SurfaceEffector2DZSaver : ZSaver.ZSaver<UnityEngine.SurfaceEffector2D> {
    public System.Single speed;
    public System.Single speedVariation;
    public System.Single forceScale;
    public System.Boolean useContactForce;
    public System.Boolean useFriction;
    public System.Boolean useBounce;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public SurfaceEffector2DZSaver (UnityEngine.SurfaceEffector2D SurfaceEffector2DInstance) : base(SurfaceEffector2DInstance.gameObject, SurfaceEffector2DInstance ) {
        speed = SurfaceEffector2DInstance.speed;
        speedVariation = SurfaceEffector2DInstance.speedVariation;
        forceScale = SurfaceEffector2DInstance.forceScale;
        useContactForce = SurfaceEffector2DInstance.useContactForce;
        useFriction = SurfaceEffector2DInstance.useFriction;
        useBounce = SurfaceEffector2DInstance.useBounce;
        useColliderMask = SurfaceEffector2DInstance.useColliderMask;
        colliderMask = SurfaceEffector2DInstance.colliderMask;
        enabled = SurfaceEffector2DInstance.enabled;
        hideFlags = SurfaceEffector2DInstance.hideFlags;

    }
}
[System.Serializable]
public class PhysicsUpdateBehaviour2DZSaver : ZSaver.ZSaver<UnityEngine.PhysicsUpdateBehaviour2D> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public PhysicsUpdateBehaviour2DZSaver (UnityEngine.PhysicsUpdateBehaviour2D PhysicsUpdateBehaviour2DInstance) : base(PhysicsUpdateBehaviour2DInstance.gameObject, PhysicsUpdateBehaviour2DInstance ) {
        enabled = PhysicsUpdateBehaviour2DInstance.enabled;
        hideFlags = PhysicsUpdateBehaviour2DInstance.hideFlags;

    }
}
[System.Serializable]
public class ConstantForce2DZSaver : ZSaver.ZSaver<UnityEngine.ConstantForce2D> {
    public UnityEngine.Vector2 force;
    public UnityEngine.Vector2 relativeForce;
    public System.Single torque;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ConstantForce2DZSaver (UnityEngine.ConstantForce2D ConstantForce2DInstance) : base(ConstantForce2DInstance.gameObject, ConstantForce2DInstance ) {
        force = ConstantForce2DInstance.force;
        relativeForce = ConstantForce2DInstance.relativeForce;
        torque = ConstantForce2DInstance.torque;
        enabled = ConstantForce2DInstance.enabled;
        hideFlags = ConstantForce2DInstance.hideFlags;

    }
}
[System.Serializable]
public class SpriteMaskZSaver : ZSaver.ZSaver<UnityEngine.SpriteMask> {
    public System.Int32 frontSortingLayerID;
    public System.Int32 frontSortingOrder;
    public System.Int32 backSortingLayerID;
    public System.Int32 backSortingOrder;
    public System.Single alphaCutoff;
    public UnityEngine.Sprite sprite;
    public System.Boolean isCustomRangeActive;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public SpriteMaskZSaver (UnityEngine.SpriteMask SpriteMaskInstance) : base(SpriteMaskInstance.gameObject, SpriteMaskInstance ) {
        frontSortingLayerID = SpriteMaskInstance.frontSortingLayerID;
        frontSortingOrder = SpriteMaskInstance.frontSortingOrder;
        backSortingLayerID = SpriteMaskInstance.backSortingLayerID;
        backSortingOrder = SpriteMaskInstance.backSortingOrder;
        alphaCutoff = SpriteMaskInstance.alphaCutoff;
        sprite = SpriteMaskInstance.sprite;
        isCustomRangeActive = SpriteMaskInstance.isCustomRangeActive;
        spriteSortPoint = SpriteMaskInstance.spriteSortPoint;
        enabled = SpriteMaskInstance.enabled;
        shadowCastingMode = SpriteMaskInstance.shadowCastingMode;
        receiveShadows = SpriteMaskInstance.receiveShadows;
        forceRenderingOff = SpriteMaskInstance.forceRenderingOff;
        motionVectorGenerationMode = SpriteMaskInstance.motionVectorGenerationMode;
        lightProbeUsage = SpriteMaskInstance.lightProbeUsage;
        reflectionProbeUsage = SpriteMaskInstance.reflectionProbeUsage;
        renderingLayerMask = SpriteMaskInstance.renderingLayerMask;
        rendererPriority = SpriteMaskInstance.rendererPriority;
        rayTracingMode = SpriteMaskInstance.rayTracingMode;
        sortingLayerName = SpriteMaskInstance.sortingLayerName;
        sortingLayerID = SpriteMaskInstance.sortingLayerID;
        sortingOrder = SpriteMaskInstance.sortingOrder;
        allowOcclusionWhenDynamic = SpriteMaskInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SpriteMaskInstance.lightProbeProxyVolumeOverride;
        probeAnchor = SpriteMaskInstance.probeAnchor;
        lightmapIndex = SpriteMaskInstance.lightmapIndex;
        realtimeLightmapIndex = SpriteMaskInstance.realtimeLightmapIndex;
        lightmapScaleOffset = SpriteMaskInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SpriteMaskInstance.realtimeLightmapScaleOffset;
        sharedMaterials = SpriteMaskInstance.sharedMaterials;
        hideFlags = SpriteMaskInstance.hideFlags;

    }
}
[System.Serializable]
public class SpriteShapeRendererZSaver : ZSaver.ZSaver<UnityEngine.U2D.SpriteShapeRenderer> {
    public UnityEngine.Color color;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public SpriteShapeRendererZSaver (UnityEngine.U2D.SpriteShapeRenderer SpriteShapeRendererInstance) : base(SpriteShapeRendererInstance.gameObject, SpriteShapeRendererInstance ) {
        color = SpriteShapeRendererInstance.color;
        maskInteraction = SpriteShapeRendererInstance.maskInteraction;
        enabled = SpriteShapeRendererInstance.enabled;
        shadowCastingMode = SpriteShapeRendererInstance.shadowCastingMode;
        receiveShadows = SpriteShapeRendererInstance.receiveShadows;
        forceRenderingOff = SpriteShapeRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = SpriteShapeRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = SpriteShapeRendererInstance.lightProbeUsage;
        reflectionProbeUsage = SpriteShapeRendererInstance.reflectionProbeUsage;
        renderingLayerMask = SpriteShapeRendererInstance.renderingLayerMask;
        rendererPriority = SpriteShapeRendererInstance.rendererPriority;
        rayTracingMode = SpriteShapeRendererInstance.rayTracingMode;
        sortingLayerName = SpriteShapeRendererInstance.sortingLayerName;
        sortingLayerID = SpriteShapeRendererInstance.sortingLayerID;
        sortingOrder = SpriteShapeRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = SpriteShapeRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SpriteShapeRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = SpriteShapeRendererInstance.probeAnchor;
        lightmapIndex = SpriteShapeRendererInstance.lightmapIndex;
        realtimeLightmapIndex = SpriteShapeRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = SpriteShapeRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SpriteShapeRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = SpriteShapeRendererInstance.sharedMaterials;
        hideFlags = SpriteShapeRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class StreamingControllerZSaver : ZSaver.ZSaver<UnityEngine.StreamingController> {
    public System.Single streamingMipmapBias;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public StreamingControllerZSaver (UnityEngine.StreamingController StreamingControllerInstance) : base(StreamingControllerInstance.gameObject, StreamingControllerInstance ) {
        streamingMipmapBias = StreamingControllerInstance.streamingMipmapBias;
        enabled = StreamingControllerInstance.enabled;
        hideFlags = StreamingControllerInstance.hideFlags;

    }
}
[System.Serializable]
public class TerrainZSaver : ZSaver.ZSaver<UnityEngine.Terrain> {
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
    public System.Boolean drawTreesAndFoliage;
    public UnityEngine.Vector3 patchBoundsMultiplier;
    public System.Single treeLODBiasMultiplier;
    public System.Boolean collectDetailPatches;
    public UnityEngine.TerrainRenderFlags editorRenderFlags;
    public System.Boolean preserveTreePrototypeLayers;
    public System.UInt32 renderingLayerMask;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public TerrainZSaver (UnityEngine.Terrain TerrainInstance) : base(TerrainInstance.gameObject, TerrainInstance ) {
        terrainData = TerrainInstance.terrainData;
        treeDistance = TerrainInstance.treeDistance;
        treeBillboardDistance = TerrainInstance.treeBillboardDistance;
        treeCrossFadeLength = TerrainInstance.treeCrossFadeLength;
        treeMaximumFullLODCount = TerrainInstance.treeMaximumFullLODCount;
        detailObjectDistance = TerrainInstance.detailObjectDistance;
        detailObjectDensity = TerrainInstance.detailObjectDensity;
        heightmapPixelError = TerrainInstance.heightmapPixelError;
        heightmapMaximumLOD = TerrainInstance.heightmapMaximumLOD;
        basemapDistance = TerrainInstance.basemapDistance;
        lightmapIndex = TerrainInstance.lightmapIndex;
        realtimeLightmapIndex = TerrainInstance.realtimeLightmapIndex;
        lightmapScaleOffset = TerrainInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = TerrainInstance.realtimeLightmapScaleOffset;
        freeUnusedRenderingResources = TerrainInstance.freeUnusedRenderingResources;
        shadowCastingMode = TerrainInstance.shadowCastingMode;
        reflectionProbeUsage = TerrainInstance.reflectionProbeUsage;
        materialTemplate = TerrainInstance.materialTemplate;
        drawHeightmap = TerrainInstance.drawHeightmap;
        allowAutoConnect = TerrainInstance.allowAutoConnect;
        groupingID = TerrainInstance.groupingID;
        drawInstanced = TerrainInstance.drawInstanced;
        drawTreesAndFoliage = TerrainInstance.drawTreesAndFoliage;
        patchBoundsMultiplier = TerrainInstance.patchBoundsMultiplier;
        treeLODBiasMultiplier = TerrainInstance.treeLODBiasMultiplier;
        collectDetailPatches = TerrainInstance.collectDetailPatches;
        editorRenderFlags = TerrainInstance.editorRenderFlags;
        preserveTreePrototypeLayers = TerrainInstance.preserveTreePrototypeLayers;
        renderingLayerMask = TerrainInstance.renderingLayerMask;
        enabled = TerrainInstance.enabled;
        hideFlags = TerrainInstance.hideFlags;

    }
}
[System.Serializable]
public class TreeZSaver : ZSaver.ZSaver<UnityEngine.Tree> {
    public UnityEngine.ScriptableObject data;
    public UnityEngine.HideFlags hideFlags;
    public TreeZSaver (UnityEngine.Tree TreeInstance) : base(TreeInstance.gameObject, TreeInstance ) {
        data = TreeInstance.data;
        hideFlags = TreeInstance.hideFlags;

    }
}
[System.Serializable]
public class TerrainColliderZSaver : ZSaver.ZSaver<UnityEngine.TerrainCollider> {
    public UnityEngine.TerrainData terrainData;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public TerrainColliderZSaver (UnityEngine.TerrainCollider TerrainColliderInstance) : base(TerrainColliderInstance.gameObject, TerrainColliderInstance ) {
        terrainData = TerrainColliderInstance.terrainData;
        enabled = TerrainColliderInstance.enabled;
        isTrigger = TerrainColliderInstance.isTrigger;
        contactOffset = TerrainColliderInstance.contactOffset;
        hideFlags = TerrainColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class TextMeshZSaver : ZSaver.ZSaver<UnityEngine.TextMesh> {
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
    public UnityEngine.HideFlags hideFlags;
    public TextMeshZSaver (UnityEngine.TextMesh TextMeshInstance) : base(TextMeshInstance.gameObject, TextMeshInstance ) {
        text = TextMeshInstance.text;
        font = TextMeshInstance.font;
        fontSize = TextMeshInstance.fontSize;
        fontStyle = TextMeshInstance.fontStyle;
        offsetZ = TextMeshInstance.offsetZ;
        alignment = TextMeshInstance.alignment;
        anchor = TextMeshInstance.anchor;
        characterSize = TextMeshInstance.characterSize;
        lineSpacing = TextMeshInstance.lineSpacing;
        tabSize = TextMeshInstance.tabSize;
        richText = TextMeshInstance.richText;
        color = TextMeshInstance.color;
        hideFlags = TextMeshInstance.hideFlags;

    }
}
[System.Serializable]
public class TilemapZSaver : ZSaver.ZSaver<UnityEngine.Tilemaps.Tilemap> {
    public System.Single animationFrameRate;
    public UnityEngine.Color color;
    public UnityEngine.Vector3Int origin;
    public UnityEngine.Vector3Int size;
    public UnityEngine.Vector3 tileAnchor;
    public UnityEngine.Tilemaps.Tilemap.Orientation orientation;
    public UnityEngine.Matrix4x4 orientationMatrix;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public TilemapZSaver (UnityEngine.Tilemaps.Tilemap TilemapInstance) : base(TilemapInstance.gameObject, TilemapInstance ) {
        animationFrameRate = TilemapInstance.animationFrameRate;
        color = TilemapInstance.color;
        origin = TilemapInstance.origin;
        size = TilemapInstance.size;
        tileAnchor = TilemapInstance.tileAnchor;
        orientation = TilemapInstance.orientation;
        orientationMatrix = TilemapInstance.orientationMatrix;
        enabled = TilemapInstance.enabled;
        hideFlags = TilemapInstance.hideFlags;

    }
}
[System.Serializable]
public class TilemapRendererZSaver : ZSaver.ZSaver<UnityEngine.Tilemaps.TilemapRenderer> {
    public UnityEngine.Vector3Int chunkSize;
    public UnityEngine.Vector3 chunkCullingBounds;
    public System.Int32 maxChunkCount;
    public System.Int32 maxFrameAge;
    public UnityEngine.Tilemaps.TilemapRenderer.SortOrder sortOrder;
    public UnityEngine.Tilemaps.TilemapRenderer.Mode mode;
    public UnityEngine.Tilemaps.TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public System.Boolean enabled;
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
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public TilemapRendererZSaver (UnityEngine.Tilemaps.TilemapRenderer TilemapRendererInstance) : base(TilemapRendererInstance.gameObject, TilemapRendererInstance ) {
        chunkSize = TilemapRendererInstance.chunkSize;
        chunkCullingBounds = TilemapRendererInstance.chunkCullingBounds;
        maxChunkCount = TilemapRendererInstance.maxChunkCount;
        maxFrameAge = TilemapRendererInstance.maxFrameAge;
        sortOrder = TilemapRendererInstance.sortOrder;
        mode = TilemapRendererInstance.mode;
        detectChunkCullingBounds = TilemapRendererInstance.detectChunkCullingBounds;
        maskInteraction = TilemapRendererInstance.maskInteraction;
        enabled = TilemapRendererInstance.enabled;
        shadowCastingMode = TilemapRendererInstance.shadowCastingMode;
        receiveShadows = TilemapRendererInstance.receiveShadows;
        forceRenderingOff = TilemapRendererInstance.forceRenderingOff;
        motionVectorGenerationMode = TilemapRendererInstance.motionVectorGenerationMode;
        lightProbeUsage = TilemapRendererInstance.lightProbeUsage;
        reflectionProbeUsage = TilemapRendererInstance.reflectionProbeUsage;
        renderingLayerMask = TilemapRendererInstance.renderingLayerMask;
        rendererPriority = TilemapRendererInstance.rendererPriority;
        rayTracingMode = TilemapRendererInstance.rayTracingMode;
        sortingLayerName = TilemapRendererInstance.sortingLayerName;
        sortingLayerID = TilemapRendererInstance.sortingLayerID;
        sortingOrder = TilemapRendererInstance.sortingOrder;
        allowOcclusionWhenDynamic = TilemapRendererInstance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = TilemapRendererInstance.lightProbeProxyVolumeOverride;
        probeAnchor = TilemapRendererInstance.probeAnchor;
        lightmapIndex = TilemapRendererInstance.lightmapIndex;
        realtimeLightmapIndex = TilemapRendererInstance.realtimeLightmapIndex;
        lightmapScaleOffset = TilemapRendererInstance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = TilemapRendererInstance.realtimeLightmapScaleOffset;
        sharedMaterials = TilemapRendererInstance.sharedMaterials;
        hideFlags = TilemapRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class TilemapCollider2DZSaver : ZSaver.ZSaver<UnityEngine.Tilemaps.TilemapCollider2D> {
    public System.UInt32 maximumTileChangeCount;
    public System.Single extrusionFactor;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public TilemapCollider2DZSaver (UnityEngine.Tilemaps.TilemapCollider2D TilemapCollider2DInstance) : base(TilemapCollider2DInstance.gameObject, TilemapCollider2DInstance ) {
        maximumTileChangeCount = TilemapCollider2DInstance.maximumTileChangeCount;
        extrusionFactor = TilemapCollider2DInstance.extrusionFactor;
        density = TilemapCollider2DInstance.density;
        isTrigger = TilemapCollider2DInstance.isTrigger;
        usedByEffector = TilemapCollider2DInstance.usedByEffector;
        usedByComposite = TilemapCollider2DInstance.usedByComposite;
        offset = TilemapCollider2DInstance.offset;
        enabled = TilemapCollider2DInstance.enabled;
        hideFlags = TilemapCollider2DInstance.hideFlags;

    }
}
[System.Serializable]
public class CanvasGroupZSaver : ZSaver.ZSaver<UnityEngine.CanvasGroup> {
    public System.Single alpha;
    public System.Boolean interactable;
    public System.Boolean blocksRaycasts;
    public System.Boolean ignoreParentGroups;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CanvasGroupZSaver (UnityEngine.CanvasGroup CanvasGroupInstance) : base(CanvasGroupInstance.gameObject, CanvasGroupInstance ) {
        alpha = CanvasGroupInstance.alpha;
        interactable = CanvasGroupInstance.interactable;
        blocksRaycasts = CanvasGroupInstance.blocksRaycasts;
        ignoreParentGroups = CanvasGroupInstance.ignoreParentGroups;
        enabled = CanvasGroupInstance.enabled;
        hideFlags = CanvasGroupInstance.hideFlags;

    }
}
[System.Serializable]
public class CanvasRendererZSaver : ZSaver.ZSaver<UnityEngine.CanvasRenderer> {
    public System.Boolean hasPopInstruction;
    public System.Int32 materialCount;
    public System.Int32 popMaterialCount;
    public System.Boolean cullTransparentMesh;
    public System.Boolean cull;
    public UnityEngine.Vector2 clippingSoftness;
    public UnityEngine.HideFlags hideFlags;
    public CanvasRendererZSaver (UnityEngine.CanvasRenderer CanvasRendererInstance) : base(CanvasRendererInstance.gameObject, CanvasRendererInstance ) {
        hasPopInstruction = CanvasRendererInstance.hasPopInstruction;
        materialCount = CanvasRendererInstance.materialCount;
        popMaterialCount = CanvasRendererInstance.popMaterialCount;
        cullTransparentMesh = CanvasRendererInstance.cullTransparentMesh;
        cull = CanvasRendererInstance.cull;
        clippingSoftness = CanvasRendererInstance.clippingSoftness;
        hideFlags = CanvasRendererInstance.hideFlags;

    }
}
[System.Serializable]
public class CanvasZSaver : ZSaver.ZSaver<UnityEngine.Canvas> {
    public UnityEngine.RenderMode renderMode;
    public System.Single scaleFactor;
    public System.Single referencePixelsPerUnit;
    public System.Boolean overridePixelPerfect;
    public System.Boolean pixelPerfect;
    public System.Single planeDistance;
    public System.Boolean overrideSorting;
    public System.Int32 sortingOrder;
    public System.Int32 targetDisplay;
    public System.Int32 sortingLayerID;
    public UnityEngine.AdditionalCanvasShaderChannels additionalShaderChannels;
    public System.String sortingLayerName;
    public UnityEngine.Camera worldCamera;
    public System.Single normalizedSortingGridSize;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public CanvasZSaver (UnityEngine.Canvas CanvasInstance) : base(CanvasInstance.gameObject, CanvasInstance ) {
        renderMode = CanvasInstance.renderMode;
        scaleFactor = CanvasInstance.scaleFactor;
        referencePixelsPerUnit = CanvasInstance.referencePixelsPerUnit;
        overridePixelPerfect = CanvasInstance.overridePixelPerfect;
        pixelPerfect = CanvasInstance.pixelPerfect;
        planeDistance = CanvasInstance.planeDistance;
        overrideSorting = CanvasInstance.overrideSorting;
        sortingOrder = CanvasInstance.sortingOrder;
        targetDisplay = CanvasInstance.targetDisplay;
        sortingLayerID = CanvasInstance.sortingLayerID;
        additionalShaderChannels = CanvasInstance.additionalShaderChannels;
        sortingLayerName = CanvasInstance.sortingLayerName;
        worldCamera = CanvasInstance.worldCamera;
        normalizedSortingGridSize = CanvasInstance.normalizedSortingGridSize;
        enabled = CanvasInstance.enabled;
        hideFlags = CanvasInstance.hideFlags;

    }
}
[System.Serializable]
public class VisualEffectZSaver : ZSaver.ZSaver<UnityEngine.VFX.VisualEffect> {
    public System.Boolean pause;
    public System.Single playRate;
    public System.UInt32 startSeed;
    public System.Boolean resetSeedOnPlay;
    public System.Int32 initialEventID;
    public System.String initialEventName;
    public UnityEngine.VFX.VisualEffectAsset visualEffectAsset;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public System.Action<UnityEngine.VFX.VFXOutputEventArgs> outputEventReceived;
    public VisualEffectZSaver (UnityEngine.VFX.VisualEffect VisualEffectInstance) : base(VisualEffectInstance.gameObject, VisualEffectInstance ) {
        pause = VisualEffectInstance.pause;
        playRate = VisualEffectInstance.playRate;
        startSeed = VisualEffectInstance.startSeed;
        resetSeedOnPlay = VisualEffectInstance.resetSeedOnPlay;
        initialEventID = VisualEffectInstance.initialEventID;
        initialEventName = VisualEffectInstance.initialEventName;
        visualEffectAsset = VisualEffectInstance.visualEffectAsset;
        enabled = VisualEffectInstance.enabled;
        hideFlags = VisualEffectInstance.hideFlags;
        outputEventReceived = VisualEffectInstance.outputEventReceived;

    }
}
[System.Serializable]
public class WheelColliderZSaver : ZSaver.ZSaver<UnityEngine.WheelCollider> {
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
    public System.Single sprungMass;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.HideFlags hideFlags;
    public WheelColliderZSaver (UnityEngine.WheelCollider WheelColliderInstance) : base(WheelColliderInstance.gameObject, WheelColliderInstance ) {
        center = WheelColliderInstance.center;
        radius = WheelColliderInstance.radius;
        suspensionDistance = WheelColliderInstance.suspensionDistance;
        suspensionSpring = WheelColliderInstance.suspensionSpring;
        suspensionExpansionLimited = WheelColliderInstance.suspensionExpansionLimited;
        forceAppPointDistance = WheelColliderInstance.forceAppPointDistance;
        mass = WheelColliderInstance.mass;
        wheelDampingRate = WheelColliderInstance.wheelDampingRate;
        forwardFriction = WheelColliderInstance.forwardFriction;
        sidewaysFriction = WheelColliderInstance.sidewaysFriction;
        motorTorque = WheelColliderInstance.motorTorque;
        brakeTorque = WheelColliderInstance.brakeTorque;
        steerAngle = WheelColliderInstance.steerAngle;
        sprungMass = WheelColliderInstance.sprungMass;
        enabled = WheelColliderInstance.enabled;
        isTrigger = WheelColliderInstance.isTrigger;
        contactOffset = WheelColliderInstance.contactOffset;
        hideFlags = WheelColliderInstance.hideFlags;

    }
}
[System.Serializable]
public class VideoPlayerZSaver : ZSaver.ZSaver<UnityEngine.Video.VideoPlayer> {
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
    public System.Boolean waitForFirstFrame;
    public System.Boolean playOnAwake;
    public System.Double time;
    public System.Int64 frame;
    public System.Single playbackSpeed;
    public System.Boolean isLooping;
    public UnityEngine.Video.VideoTimeSource timeSource;
    public UnityEngine.Video.VideoTimeReference timeReference;
    public System.Double externalReferenceTime;
    public System.Boolean skipOnDrop;
    public System.UInt16 controlledAudioTrackCount;
    public UnityEngine.Video.VideoAudioOutputMode audioOutputMode;
    public System.Boolean sendFrameReadyEvents;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public VideoPlayerZSaver (UnityEngine.Video.VideoPlayer VideoPlayerInstance) : base(VideoPlayerInstance.gameObject, VideoPlayerInstance ) {
        source = VideoPlayerInstance.source;
        url = VideoPlayerInstance.url;
        clip = VideoPlayerInstance.clip;
        renderMode = VideoPlayerInstance.renderMode;
        targetCamera = VideoPlayerInstance.targetCamera;
        targetTexture = VideoPlayerInstance.targetTexture;
        targetMaterialRenderer = VideoPlayerInstance.targetMaterialRenderer;
        targetMaterialProperty = VideoPlayerInstance.targetMaterialProperty;
        aspectRatio = VideoPlayerInstance.aspectRatio;
        targetCameraAlpha = VideoPlayerInstance.targetCameraAlpha;
        targetCamera3DLayout = VideoPlayerInstance.targetCamera3DLayout;
        waitForFirstFrame = VideoPlayerInstance.waitForFirstFrame;
        playOnAwake = VideoPlayerInstance.playOnAwake;
        time = VideoPlayerInstance.time;
        frame = VideoPlayerInstance.frame;
        playbackSpeed = VideoPlayerInstance.playbackSpeed;
        isLooping = VideoPlayerInstance.isLooping;
        timeSource = VideoPlayerInstance.timeSource;
        timeReference = VideoPlayerInstance.timeReference;
        externalReferenceTime = VideoPlayerInstance.externalReferenceTime;
        skipOnDrop = VideoPlayerInstance.skipOnDrop;
        controlledAudioTrackCount = VideoPlayerInstance.controlledAudioTrackCount;
        audioOutputMode = VideoPlayerInstance.audioOutputMode;
        sendFrameReadyEvents = VideoPlayerInstance.sendFrameReadyEvents;
        enabled = VideoPlayerInstance.enabled;
        hideFlags = VideoPlayerInstance.hideFlags;

    }
}
[System.Serializable]
public class WindZoneZSaver : ZSaver.ZSaver<UnityEngine.WindZone> {
    public UnityEngine.WindZoneMode mode;
    public System.Single radius;
    public System.Single windMain;
    public System.Single windTurbulence;
    public System.Single windPulseMagnitude;
    public System.Single windPulseFrequency;
    public UnityEngine.HideFlags hideFlags;
    public WindZoneZSaver (UnityEngine.WindZone WindZoneInstance) : base(WindZoneInstance.gameObject, WindZoneInstance ) {
        mode = WindZoneInstance.mode;
        radius = WindZoneInstance.radius;
        windMain = WindZoneInstance.windMain;
        windTurbulence = WindZoneInstance.windTurbulence;
        windPulseMagnitude = WindZoneInstance.windPulseMagnitude;
        windPulseFrequency = WindZoneInstance.windPulseFrequency;
        hideFlags = WindZoneInstance.hideFlags;

    }
}
[System.Serializable]
public class PersistentGameObjectZSaver : ZSaver.ZSaver<PersistentGameObject> {
    public System.Boolean useGUILayout;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public PersistentGameObject.SerializableComponentData[] _componentDatas;
    public ZSaver.GameObjectData gameObjectData;
    public PersistentGameObjectZSaver (PersistentGameObject PersistentGameObjectInstance) : base(PersistentGameObjectInstance.gameObject, PersistentGameObjectInstance ) {
        useGUILayout = PersistentGameObjectInstance.useGUILayout;
        enabled = PersistentGameObjectInstance.enabled;
        hideFlags = PersistentGameObjectInstance.hideFlags;
        _componentDatas = PersistentGameObjectInstance._componentDatas;
        gameObjectData =new ZSaver.GameObjectData()
        {
            loadingOrder = PersistentGameObject.CountParents(PersistentGameObjectInstance.transform),
            active = _componentParent.activeSelf,
            hideFlags = _componentParent.hideFlags,
            isStatic = _componentParent.isStatic,
            layer = PersistentGameObjectInstance.gameObject.layer,
            name = _componentParent.name,
            position = _componentParent.transform.position,
            rotation = _componentParent.transform.rotation,
            size = _componentParent.transform.localScale,
            tag = PersistentGameObjectInstance.gameObject.tag,
            parent = PersistentGameObjectInstance.transform.parent ? PersistentGameObjectInstance.transform.parent.gameObject : null
        };
    }
}
