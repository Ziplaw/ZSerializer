[System.Serializable]
public class NavMeshAgentZSaver : ZSave.ZSaver<UnityEngine.AI.NavMeshAgent> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public NavMeshAgentZSaver (UnityEngine.AI.NavMeshAgent NavMeshAgent) : base(NavMeshAgent.gameObject, NavMeshAgent) {
        destination = NavMeshAgent.destination;
        stoppingDistance = NavMeshAgent.stoppingDistance;
        velocity = NavMeshAgent.velocity;
        nextPosition = NavMeshAgent.nextPosition;
        baseOffset = NavMeshAgent.baseOffset;
        autoTraverseOffMeshLink = NavMeshAgent.autoTraverseOffMeshLink;
        autoBraking = NavMeshAgent.autoBraking;
        autoRepath = NavMeshAgent.autoRepath;
        isStopped = NavMeshAgent.isStopped;
        path = NavMeshAgent.path;
        agentTypeID = NavMeshAgent.agentTypeID;
        areaMask = NavMeshAgent.areaMask;
        speed = NavMeshAgent.speed;
        angularSpeed = NavMeshAgent.angularSpeed;
        acceleration = NavMeshAgent.acceleration;
        updatePosition = NavMeshAgent.updatePosition;
        updateRotation = NavMeshAgent.updateRotation;
        updateUpAxis = NavMeshAgent.updateUpAxis;
        radius = NavMeshAgent.radius;
        height = NavMeshAgent.height;
        obstacleAvoidanceType = NavMeshAgent.obstacleAvoidanceType;
        avoidancePriority = NavMeshAgent.avoidancePriority;
        enabled = NavMeshAgent.enabled;
        tag = NavMeshAgent.tag;
        name = NavMeshAgent.name;
        hideFlags = NavMeshAgent.hideFlags;
    }
}
[System.Serializable]
public class NavMeshObstacleZSaver : ZSave.ZSaver<UnityEngine.AI.NavMeshObstacle> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public NavMeshObstacleZSaver (UnityEngine.AI.NavMeshObstacle NavMeshObstacle) : base(NavMeshObstacle.gameObject, NavMeshObstacle) {
        height = NavMeshObstacle.height;
        radius = NavMeshObstacle.radius;
        velocity = NavMeshObstacle.velocity;
        carving = NavMeshObstacle.carving;
        carveOnlyStationary = NavMeshObstacle.carveOnlyStationary;
        carvingMoveThreshold = NavMeshObstacle.carvingMoveThreshold;
        carvingTimeToStationary = NavMeshObstacle.carvingTimeToStationary;
        shape = NavMeshObstacle.shape;
        center = NavMeshObstacle.center;
        size = NavMeshObstacle.size;
        enabled = NavMeshObstacle.enabled;
        tag = NavMeshObstacle.tag;
        name = NavMeshObstacle.name;
        hideFlags = NavMeshObstacle.hideFlags;
    }
}
[System.Serializable]
public class OffMeshLinkZSaver : ZSave.ZSaver<UnityEngine.AI.OffMeshLink> {
    public System.Boolean activated;
    public System.Single costOverride;
    public System.Boolean biDirectional;
    public System.Int32 area;
    public System.Boolean autoUpdatePositions;
    public UnityEngine.Transform startTransform;
    public UnityEngine.Transform endTransform;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public OffMeshLinkZSaver (UnityEngine.AI.OffMeshLink OffMeshLink) : base(OffMeshLink.gameObject, OffMeshLink) {
        activated = OffMeshLink.activated;
        costOverride = OffMeshLink.costOverride;
        biDirectional = OffMeshLink.biDirectional;
        area = OffMeshLink.area;
        autoUpdatePositions = OffMeshLink.autoUpdatePositions;
        startTransform = OffMeshLink.startTransform;
        endTransform = OffMeshLink.endTransform;
        enabled = OffMeshLink.enabled;
        tag = OffMeshLink.tag;
        name = OffMeshLink.name;
        hideFlags = OffMeshLink.hideFlags;
    }
}
[System.Serializable]
public class AnimatorZSaver : ZSave.ZSaver<UnityEngine.Animator> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AnimatorZSaver (UnityEngine.Animator Animator) : base(Animator.gameObject, Animator) {
        rootPosition = Animator.rootPosition;
        rootRotation = Animator.rootRotation;
        applyRootMotion = Animator.applyRootMotion;
        updateMode = Animator.updateMode;
        bodyPosition = Animator.bodyPosition;
        bodyRotation = Animator.bodyRotation;
        stabilizeFeet = Animator.stabilizeFeet;
        feetPivotActive = Animator.feetPivotActive;
        speed = Animator.speed;
        cullingMode = Animator.cullingMode;
        playbackTime = Animator.playbackTime;
        recorderStartTime = Animator.recorderStartTime;
        recorderStopTime = Animator.recorderStopTime;
        runtimeAnimatorController = Animator.runtimeAnimatorController;
        avatar = Animator.avatar;
        layersAffectMassCenter = Animator.layersAffectMassCenter;
        logWarnings = Animator.logWarnings;
        fireEvents = Animator.fireEvents;
        keepAnimatorControllerStateOnDisable = Animator.keepAnimatorControllerStateOnDisable;
        enabled = Animator.enabled;
        tag = Animator.tag;
        name = Animator.name;
        hideFlags = Animator.hideFlags;
    }
}
[System.Serializable]
public class AnimationZSaver : ZSave.ZSaver<UnityEngine.Animation> {
    public UnityEngine.AnimationClip clip;
    public System.Boolean playAutomatically;
    public UnityEngine.WrapMode wrapMode;
    public System.Boolean animatePhysics;
    public UnityEngine.AnimationCullingType cullingType;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AnimationZSaver (UnityEngine.Animation Animation) : base(Animation.gameObject, Animation) {
        clip = Animation.clip;
        playAutomatically = Animation.playAutomatically;
        wrapMode = Animation.wrapMode;
        animatePhysics = Animation.animatePhysics;
        cullingType = Animation.cullingType;
        localBounds = Animation.localBounds;
        enabled = Animation.enabled;
        tag = Animation.tag;
        name = Animation.name;
        hideFlags = Animation.hideFlags;
    }
}
[System.Serializable]
public class AimConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.AimConstraint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AimConstraintZSaver (UnityEngine.Animations.AimConstraint AimConstraint) : base(AimConstraint.gameObject, AimConstraint) {
        weight = AimConstraint.weight;
        constraintActive = AimConstraint.constraintActive;
        locked = AimConstraint.locked;
        rotationAtRest = AimConstraint.rotationAtRest;
        rotationOffset = AimConstraint.rotationOffset;
        rotationAxis = AimConstraint.rotationAxis;
        aimVector = AimConstraint.aimVector;
        upVector = AimConstraint.upVector;
        worldUpVector = AimConstraint.worldUpVector;
        worldUpObject = AimConstraint.worldUpObject;
        worldUpType = AimConstraint.worldUpType;
        enabled = AimConstraint.enabled;
        tag = AimConstraint.tag;
        name = AimConstraint.name;
        hideFlags = AimConstraint.hideFlags;
    }
}
[System.Serializable]
public class PositionConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.PositionConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 translationAtRest;
    public UnityEngine.Vector3 translationOffset;
    public UnityEngine.Animations.Axis translationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PositionConstraintZSaver (UnityEngine.Animations.PositionConstraint PositionConstraint) : base(PositionConstraint.gameObject, PositionConstraint) {
        weight = PositionConstraint.weight;
        translationAtRest = PositionConstraint.translationAtRest;
        translationOffset = PositionConstraint.translationOffset;
        translationAxis = PositionConstraint.translationAxis;
        constraintActive = PositionConstraint.constraintActive;
        locked = PositionConstraint.locked;
        enabled = PositionConstraint.enabled;
        tag = PositionConstraint.tag;
        name = PositionConstraint.name;
        hideFlags = PositionConstraint.hideFlags;
    }
}
[System.Serializable]
public class RotationConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.RotationConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Animations.Axis rotationAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public RotationConstraintZSaver (UnityEngine.Animations.RotationConstraint RotationConstraint) : base(RotationConstraint.gameObject, RotationConstraint) {
        weight = RotationConstraint.weight;
        rotationAtRest = RotationConstraint.rotationAtRest;
        rotationOffset = RotationConstraint.rotationOffset;
        rotationAxis = RotationConstraint.rotationAxis;
        constraintActive = RotationConstraint.constraintActive;
        locked = RotationConstraint.locked;
        enabled = RotationConstraint.enabled;
        tag = RotationConstraint.tag;
        name = RotationConstraint.name;
        hideFlags = RotationConstraint.hideFlags;
    }
}
[System.Serializable]
public class ScaleConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.ScaleConstraint> {
    public System.Single weight;
    public UnityEngine.Vector3 scaleAtRest;
    public UnityEngine.Vector3 scaleOffset;
    public UnityEngine.Animations.Axis scalingAxis;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ScaleConstraintZSaver (UnityEngine.Animations.ScaleConstraint ScaleConstraint) : base(ScaleConstraint.gameObject, ScaleConstraint) {
        weight = ScaleConstraint.weight;
        scaleAtRest = ScaleConstraint.scaleAtRest;
        scaleOffset = ScaleConstraint.scaleOffset;
        scalingAxis = ScaleConstraint.scalingAxis;
        constraintActive = ScaleConstraint.constraintActive;
        locked = ScaleConstraint.locked;
        enabled = ScaleConstraint.enabled;
        tag = ScaleConstraint.tag;
        name = ScaleConstraint.name;
        hideFlags = ScaleConstraint.hideFlags;
    }
}
[System.Serializable]
public class LookAtConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.LookAtConstraint> {
    public System.Single weight;
    public System.Single roll;
    public System.Boolean constraintActive;
    public System.Boolean locked;
    public UnityEngine.Vector3 rotationAtRest;
    public UnityEngine.Vector3 rotationOffset;
    public UnityEngine.Transform worldUpObject;
    public System.Boolean useUpObject;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LookAtConstraintZSaver (UnityEngine.Animations.LookAtConstraint LookAtConstraint) : base(LookAtConstraint.gameObject, LookAtConstraint) {
        weight = LookAtConstraint.weight;
        roll = LookAtConstraint.roll;
        constraintActive = LookAtConstraint.constraintActive;
        locked = LookAtConstraint.locked;
        rotationAtRest = LookAtConstraint.rotationAtRest;
        rotationOffset = LookAtConstraint.rotationOffset;
        worldUpObject = LookAtConstraint.worldUpObject;
        useUpObject = LookAtConstraint.useUpObject;
        enabled = LookAtConstraint.enabled;
        tag = LookAtConstraint.tag;
        name = LookAtConstraint.name;
        hideFlags = LookAtConstraint.hideFlags;
    }
}
[System.Serializable]
public class ParentConstraintZSaver : ZSave.ZSaver<UnityEngine.Animations.ParentConstraint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ParentConstraintZSaver (UnityEngine.Animations.ParentConstraint ParentConstraint) : base(ParentConstraint.gameObject, ParentConstraint) {
        weight = ParentConstraint.weight;
        constraintActive = ParentConstraint.constraintActive;
        locked = ParentConstraint.locked;
        translationAtRest = ParentConstraint.translationAtRest;
        rotationAtRest = ParentConstraint.rotationAtRest;
        translationOffsets = ParentConstraint.translationOffsets;
        rotationOffsets = ParentConstraint.rotationOffsets;
        translationAxis = ParentConstraint.translationAxis;
        rotationAxis = ParentConstraint.rotationAxis;
        enabled = ParentConstraint.enabled;
        tag = ParentConstraint.tag;
        name = ParentConstraint.name;
        hideFlags = ParentConstraint.hideFlags;
    }
}
[System.Serializable]
public class AudioSourceZSaver : ZSave.ZSaver<UnityEngine.AudioSource> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioSourceZSaver (UnityEngine.AudioSource AudioSource) : base(AudioSource.gameObject, AudioSource) {
        volume = AudioSource.volume;
        pitch = AudioSource.pitch;
        time = AudioSource.time;
        timeSamples = AudioSource.timeSamples;
        clip = AudioSource.clip;
        outputAudioMixerGroup = AudioSource.outputAudioMixerGroup;
        loop = AudioSource.loop;
        ignoreListenerVolume = AudioSource.ignoreListenerVolume;
        playOnAwake = AudioSource.playOnAwake;
        ignoreListenerPause = AudioSource.ignoreListenerPause;
        velocityUpdateMode = AudioSource.velocityUpdateMode;
        panStereo = AudioSource.panStereo;
        spatialBlend = AudioSource.spatialBlend;
        spatialize = AudioSource.spatialize;
        spatializePostEffects = AudioSource.spatializePostEffects;
        reverbZoneMix = AudioSource.reverbZoneMix;
        bypassEffects = AudioSource.bypassEffects;
        bypassListenerEffects = AudioSource.bypassListenerEffects;
        bypassReverbZones = AudioSource.bypassReverbZones;
        dopplerLevel = AudioSource.dopplerLevel;
        spread = AudioSource.spread;
        priority = AudioSource.priority;
        mute = AudioSource.mute;
        minDistance = AudioSource.minDistance;
        maxDistance = AudioSource.maxDistance;
        rolloffMode = AudioSource.rolloffMode;
        enabled = AudioSource.enabled;
        tag = AudioSource.tag;
        name = AudioSource.name;
        hideFlags = AudioSource.hideFlags;
    }
}
[System.Serializable]
public class AudioLowPassFilterZSaver : ZSave.ZSaver<UnityEngine.AudioLowPassFilter> {
    public UnityEngine.AnimationCurve customCutoffCurve;
    public System.Single cutoffFrequency;
    public System.Single lowpassResonanceQ;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioLowPassFilterZSaver (UnityEngine.AudioLowPassFilter AudioLowPassFilter) : base(AudioLowPassFilter.gameObject, AudioLowPassFilter) {
        customCutoffCurve = AudioLowPassFilter.customCutoffCurve;
        cutoffFrequency = AudioLowPassFilter.cutoffFrequency;
        lowpassResonanceQ = AudioLowPassFilter.lowpassResonanceQ;
        enabled = AudioLowPassFilter.enabled;
        tag = AudioLowPassFilter.tag;
        name = AudioLowPassFilter.name;
        hideFlags = AudioLowPassFilter.hideFlags;
    }
}
[System.Serializable]
public class AudioHighPassFilterZSaver : ZSave.ZSaver<UnityEngine.AudioHighPassFilter> {
    public System.Single cutoffFrequency;
    public System.Single highpassResonanceQ;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioHighPassFilterZSaver (UnityEngine.AudioHighPassFilter AudioHighPassFilter) : base(AudioHighPassFilter.gameObject, AudioHighPassFilter) {
        cutoffFrequency = AudioHighPassFilter.cutoffFrequency;
        highpassResonanceQ = AudioHighPassFilter.highpassResonanceQ;
        enabled = AudioHighPassFilter.enabled;
        tag = AudioHighPassFilter.tag;
        name = AudioHighPassFilter.name;
        hideFlags = AudioHighPassFilter.hideFlags;
    }
}
[System.Serializable]
public class AudioReverbFilterZSaver : ZSave.ZSaver<UnityEngine.AudioReverbFilter> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioReverbFilterZSaver (UnityEngine.AudioReverbFilter AudioReverbFilter) : base(AudioReverbFilter.gameObject, AudioReverbFilter) {
        reverbPreset = AudioReverbFilter.reverbPreset;
        dryLevel = AudioReverbFilter.dryLevel;
        room = AudioReverbFilter.room;
        roomHF = AudioReverbFilter.roomHF;
        decayTime = AudioReverbFilter.decayTime;
        decayHFRatio = AudioReverbFilter.decayHFRatio;
        reflectionsLevel = AudioReverbFilter.reflectionsLevel;
        reflectionsDelay = AudioReverbFilter.reflectionsDelay;
        reverbLevel = AudioReverbFilter.reverbLevel;
        reverbDelay = AudioReverbFilter.reverbDelay;
        diffusion = AudioReverbFilter.diffusion;
        density = AudioReverbFilter.density;
        hfReference = AudioReverbFilter.hfReference;
        roomLF = AudioReverbFilter.roomLF;
        lfReference = AudioReverbFilter.lfReference;
        enabled = AudioReverbFilter.enabled;
        tag = AudioReverbFilter.tag;
        name = AudioReverbFilter.name;
        hideFlags = AudioReverbFilter.hideFlags;
    }
}
[System.Serializable]
public class AudioBehaviourZSaver : ZSave.ZSaver<UnityEngine.AudioBehaviour> {
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioBehaviourZSaver (UnityEngine.AudioBehaviour AudioBehaviour) : base(AudioBehaviour.gameObject, AudioBehaviour) {
        enabled = AudioBehaviour.enabled;
        tag = AudioBehaviour.tag;
        name = AudioBehaviour.name;
        hideFlags = AudioBehaviour.hideFlags;
    }
}
[System.Serializable]
public class AudioListenerZSaver : ZSave.ZSaver<UnityEngine.AudioListener> {
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioListenerZSaver (UnityEngine.AudioListener AudioListener) : base(AudioListener.gameObject, AudioListener) {
        velocityUpdateMode = AudioListener.velocityUpdateMode;
        enabled = AudioListener.enabled;
        tag = AudioListener.tag;
        name = AudioListener.name;
        hideFlags = AudioListener.hideFlags;
    }
}
[System.Serializable]
public class AudioReverbZoneZSaver : ZSave.ZSaver<UnityEngine.AudioReverbZone> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioReverbZoneZSaver (UnityEngine.AudioReverbZone AudioReverbZone) : base(AudioReverbZone.gameObject, AudioReverbZone) {
        minDistance = AudioReverbZone.minDistance;
        maxDistance = AudioReverbZone.maxDistance;
        reverbPreset = AudioReverbZone.reverbPreset;
        room = AudioReverbZone.room;
        roomHF = AudioReverbZone.roomHF;
        roomLF = AudioReverbZone.roomLF;
        decayTime = AudioReverbZone.decayTime;
        decayHFRatio = AudioReverbZone.decayHFRatio;
        reflections = AudioReverbZone.reflections;
        reflectionsDelay = AudioReverbZone.reflectionsDelay;
        reverb = AudioReverbZone.reverb;
        reverbDelay = AudioReverbZone.reverbDelay;
        HFReference = AudioReverbZone.HFReference;
        LFReference = AudioReverbZone.LFReference;
        diffusion = AudioReverbZone.diffusion;
        density = AudioReverbZone.density;
        enabled = AudioReverbZone.enabled;
        tag = AudioReverbZone.tag;
        name = AudioReverbZone.name;
        hideFlags = AudioReverbZone.hideFlags;
    }
}
[System.Serializable]
public class AudioDistortionFilterZSaver : ZSave.ZSaver<UnityEngine.AudioDistortionFilter> {
    public System.Single distortionLevel;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioDistortionFilterZSaver (UnityEngine.AudioDistortionFilter AudioDistortionFilter) : base(AudioDistortionFilter.gameObject, AudioDistortionFilter) {
        distortionLevel = AudioDistortionFilter.distortionLevel;
        enabled = AudioDistortionFilter.enabled;
        tag = AudioDistortionFilter.tag;
        name = AudioDistortionFilter.name;
        hideFlags = AudioDistortionFilter.hideFlags;
    }
}
[System.Serializable]
public class AudioEchoFilterZSaver : ZSave.ZSaver<UnityEngine.AudioEchoFilter> {
    public System.Single delay;
    public System.Single decayRatio;
    public System.Single dryMix;
    public System.Single wetMix;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioEchoFilterZSaver (UnityEngine.AudioEchoFilter AudioEchoFilter) : base(AudioEchoFilter.gameObject, AudioEchoFilter) {
        delay = AudioEchoFilter.delay;
        decayRatio = AudioEchoFilter.decayRatio;
        dryMix = AudioEchoFilter.dryMix;
        wetMix = AudioEchoFilter.wetMix;
        enabled = AudioEchoFilter.enabled;
        tag = AudioEchoFilter.tag;
        name = AudioEchoFilter.name;
        hideFlags = AudioEchoFilter.hideFlags;
    }
}
[System.Serializable]
public class AudioChorusFilterZSaver : ZSave.ZSaver<UnityEngine.AudioChorusFilter> {
    public System.Single dryMix;
    public System.Single wetMix1;
    public System.Single wetMix2;
    public System.Single wetMix3;
    public System.Single delay;
    public System.Single rate;
    public System.Single depth;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AudioChorusFilterZSaver (UnityEngine.AudioChorusFilter AudioChorusFilter) : base(AudioChorusFilter.gameObject, AudioChorusFilter) {
        dryMix = AudioChorusFilter.dryMix;
        wetMix1 = AudioChorusFilter.wetMix1;
        wetMix2 = AudioChorusFilter.wetMix2;
        wetMix3 = AudioChorusFilter.wetMix3;
        delay = AudioChorusFilter.delay;
        rate = AudioChorusFilter.rate;
        depth = AudioChorusFilter.depth;
        enabled = AudioChorusFilter.enabled;
        tag = AudioChorusFilter.tag;
        name = AudioChorusFilter.name;
        hideFlags = AudioChorusFilter.hideFlags;
    }
}
[System.Serializable]
public class ClothZSaver : ZSave.ZSaver<UnityEngine.Cloth> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ClothZSaver (UnityEngine.Cloth Cloth) : base(Cloth.gameObject, Cloth) {
        coefficients = Cloth.coefficients;
        capsuleColliders = Cloth.capsuleColliders;
        sphereColliders = Cloth.sphereColliders;
        sleepThreshold = Cloth.sleepThreshold;
        bendingStiffness = Cloth.bendingStiffness;
        stretchingStiffness = Cloth.stretchingStiffness;
        damping = Cloth.damping;
        externalAcceleration = Cloth.externalAcceleration;
        randomAcceleration = Cloth.randomAcceleration;
        useGravity = Cloth.useGravity;
        enabled = Cloth.enabled;
        friction = Cloth.friction;
        collisionMassScale = Cloth.collisionMassScale;
        enableContinuousCollision = Cloth.enableContinuousCollision;
        useVirtualParticles = Cloth.useVirtualParticles;
        worldVelocityScale = Cloth.worldVelocityScale;
        worldAccelerationScale = Cloth.worldAccelerationScale;
        clothSolverFrequency = Cloth.clothSolverFrequency;
        useTethers = Cloth.useTethers;
        stiffnessFrequency = Cloth.stiffnessFrequency;
        selfCollisionDistance = Cloth.selfCollisionDistance;
        selfCollisionStiffness = Cloth.selfCollisionStiffness;
        tag = Cloth.tag;
        name = Cloth.name;
        hideFlags = Cloth.hideFlags;
    }
}
[System.Serializable]
public class CameraZSaver : ZSave.ZSaver<UnityEngine.Camera> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CameraZSaver (UnityEngine.Camera Camera) : base(Camera.gameObject, Camera) {
        nearClipPlane = Camera.nearClipPlane;
        farClipPlane = Camera.farClipPlane;
        fieldOfView = Camera.fieldOfView;
        renderingPath = Camera.renderingPath;
        allowHDR = Camera.allowHDR;
        allowMSAA = Camera.allowMSAA;
        allowDynamicResolution = Camera.allowDynamicResolution;
        forceIntoRenderTexture = Camera.forceIntoRenderTexture;
        orthographicSize = Camera.orthographicSize;
        orthographic = Camera.orthographic;
        opaqueSortMode = Camera.opaqueSortMode;
        transparencySortMode = Camera.transparencySortMode;
        transparencySortAxis = Camera.transparencySortAxis;
        depth = Camera.depth;
        aspect = Camera.aspect;
        cullingMask = Camera.cullingMask;
        eventMask = Camera.eventMask;
        layerCullSpherical = Camera.layerCullSpherical;
        cameraType = Camera.cameraType;
        overrideSceneCullingMask = Camera.overrideSceneCullingMask;
        layerCullDistances = Camera.layerCullDistances;
        useOcclusionCulling = Camera.useOcclusionCulling;
        cullingMatrix = Camera.cullingMatrix;
        backgroundColor = Camera.backgroundColor;
        clearFlags = Camera.clearFlags;
        depthTextureMode = Camera.depthTextureMode;
        clearStencilAfterLightingPass = Camera.clearStencilAfterLightingPass;
        usePhysicalProperties = Camera.usePhysicalProperties;
        sensorSize = Camera.sensorSize;
        lensShift = Camera.lensShift;
        focalLength = Camera.focalLength;
        gateFit = Camera.gateFit;
        rect = Camera.rect;
        pixelRect = Camera.pixelRect;
        targetTexture = Camera.targetTexture;
        targetDisplay = Camera.targetDisplay;
        worldToCameraMatrix = Camera.worldToCameraMatrix;
        projectionMatrix = Camera.projectionMatrix;
        nonJitteredProjectionMatrix = Camera.nonJitteredProjectionMatrix;
        useJitteredProjectionMatrixForTransparentRendering = Camera.useJitteredProjectionMatrixForTransparentRendering;
        scene = Camera.scene;
        stereoSeparation = Camera.stereoSeparation;
        stereoConvergence = Camera.stereoConvergence;
        stereoTargetEye = Camera.stereoTargetEye;
        enabled = Camera.enabled;
        tag = Camera.tag;
        name = Camera.name;
        hideFlags = Camera.hideFlags;
    }
}
[System.Serializable]
public class FlareLayerZSaver : ZSave.ZSaver<UnityEngine.FlareLayer> {
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public FlareLayerZSaver (UnityEngine.FlareLayer FlareLayer) : base(FlareLayer.gameObject, FlareLayer) {
        enabled = FlareLayer.enabled;
        tag = FlareLayer.tag;
        name = FlareLayer.name;
        hideFlags = FlareLayer.hideFlags;
    }
}
[System.Serializable]
public class ReflectionProbeZSaver : ZSave.ZSaver<UnityEngine.ReflectionProbe> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ReflectionProbeZSaver (UnityEngine.ReflectionProbe ReflectionProbe) : base(ReflectionProbe.gameObject, ReflectionProbe) {
        size = ReflectionProbe.size;
        center = ReflectionProbe.center;
        nearClipPlane = ReflectionProbe.nearClipPlane;
        farClipPlane = ReflectionProbe.farClipPlane;
        intensity = ReflectionProbe.intensity;
        hdr = ReflectionProbe.hdr;
        renderDynamicObjects = ReflectionProbe.renderDynamicObjects;
        shadowDistance = ReflectionProbe.shadowDistance;
        resolution = ReflectionProbe.resolution;
        cullingMask = ReflectionProbe.cullingMask;
        clearFlags = ReflectionProbe.clearFlags;
        backgroundColor = ReflectionProbe.backgroundColor;
        blendDistance = ReflectionProbe.blendDistance;
        boxProjection = ReflectionProbe.boxProjection;
        mode = ReflectionProbe.mode;
        importance = ReflectionProbe.importance;
        refreshMode = ReflectionProbe.refreshMode;
        timeSlicingMode = ReflectionProbe.timeSlicingMode;
        bakedTexture = ReflectionProbe.bakedTexture;
        customBakedTexture = ReflectionProbe.customBakedTexture;
        realtimeTexture = ReflectionProbe.realtimeTexture;
        enabled = ReflectionProbe.enabled;
        tag = ReflectionProbe.tag;
        name = ReflectionProbe.name;
        hideFlags = ReflectionProbe.hideFlags;
    }
}
[System.Serializable]
public class BillboardRendererZSaver : ZSave.ZSaver<UnityEngine.BillboardRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public BillboardRendererZSaver (UnityEngine.BillboardRenderer BillboardRenderer) : base(BillboardRenderer.gameObject, BillboardRenderer) {
        billboard = BillboardRenderer.billboard;
        enabled = BillboardRenderer.enabled;
        shadowCastingMode = BillboardRenderer.shadowCastingMode;
        receiveShadows = BillboardRenderer.receiveShadows;
        forceRenderingOff = BillboardRenderer.forceRenderingOff;
        motionVectorGenerationMode = BillboardRenderer.motionVectorGenerationMode;
        lightProbeUsage = BillboardRenderer.lightProbeUsage;
        reflectionProbeUsage = BillboardRenderer.reflectionProbeUsage;
        renderingLayerMask = BillboardRenderer.renderingLayerMask;
        rendererPriority = BillboardRenderer.rendererPriority;
        rayTracingMode = BillboardRenderer.rayTracingMode;
        sortingLayerName = BillboardRenderer.sortingLayerName;
        sortingLayerID = BillboardRenderer.sortingLayerID;
        sortingOrder = BillboardRenderer.sortingOrder;
        allowOcclusionWhenDynamic = BillboardRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = BillboardRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = BillboardRenderer.probeAnchor;
        lightmapIndex = BillboardRenderer.lightmapIndex;
        realtimeLightmapIndex = BillboardRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = BillboardRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = BillboardRenderer.realtimeLightmapScaleOffset;
        materials = BillboardRenderer.materials;
        material = BillboardRenderer.material;
        sharedMaterial = BillboardRenderer.sharedMaterial;
        sharedMaterials = BillboardRenderer.sharedMaterials;
        tag = BillboardRenderer.tag;
        name = BillboardRenderer.name;
        hideFlags = BillboardRenderer.hideFlags;
    }
}
[System.Serializable]
public class RendererZSaver : ZSave.ZSaver<UnityEngine.Renderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public RendererZSaver (UnityEngine.Renderer Renderer) : base(Renderer.gameObject, Renderer) {
        enabled = Renderer.enabled;
        shadowCastingMode = Renderer.shadowCastingMode;
        receiveShadows = Renderer.receiveShadows;
        forceRenderingOff = Renderer.forceRenderingOff;
        motionVectorGenerationMode = Renderer.motionVectorGenerationMode;
        lightProbeUsage = Renderer.lightProbeUsage;
        reflectionProbeUsage = Renderer.reflectionProbeUsage;
        renderingLayerMask = Renderer.renderingLayerMask;
        rendererPriority = Renderer.rendererPriority;
        rayTracingMode = Renderer.rayTracingMode;
        sortingLayerName = Renderer.sortingLayerName;
        sortingLayerID = Renderer.sortingLayerID;
        sortingOrder = Renderer.sortingOrder;
        allowOcclusionWhenDynamic = Renderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = Renderer.lightProbeProxyVolumeOverride;
        probeAnchor = Renderer.probeAnchor;
        lightmapIndex = Renderer.lightmapIndex;
        realtimeLightmapIndex = Renderer.realtimeLightmapIndex;
        lightmapScaleOffset = Renderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = Renderer.realtimeLightmapScaleOffset;
        materials = Renderer.materials;
        material = Renderer.material;
        sharedMaterial = Renderer.sharedMaterial;
        sharedMaterials = Renderer.sharedMaterials;
        tag = Renderer.tag;
        name = Renderer.name;
        hideFlags = Renderer.hideFlags;
    }
}
[System.Serializable]
public class ProjectorZSaver : ZSave.ZSaver<UnityEngine.Projector> {
    public System.Single nearClipPlane;
    public System.Single farClipPlane;
    public System.Single fieldOfView;
    public System.Single aspectRatio;
    public System.Boolean orthographic;
    public System.Single orthographicSize;
    public System.Int32 ignoreLayers;
    public UnityEngine.Material material;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ProjectorZSaver (UnityEngine.Projector Projector) : base(Projector.gameObject, Projector) {
        nearClipPlane = Projector.nearClipPlane;
        farClipPlane = Projector.farClipPlane;
        fieldOfView = Projector.fieldOfView;
        aspectRatio = Projector.aspectRatio;
        orthographic = Projector.orthographic;
        orthographicSize = Projector.orthographicSize;
        ignoreLayers = Projector.ignoreLayers;
        material = Projector.material;
        enabled = Projector.enabled;
        tag = Projector.tag;
        name = Projector.name;
        hideFlags = Projector.hideFlags;
    }
}
[System.Serializable]
public class TrailRendererZSaver : ZSave.ZSaver<UnityEngine.TrailRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TrailRendererZSaver (UnityEngine.TrailRenderer TrailRenderer) : base(TrailRenderer.gameObject, TrailRenderer) {
        time = TrailRenderer.time;
        startWidth = TrailRenderer.startWidth;
        endWidth = TrailRenderer.endWidth;
        widthMultiplier = TrailRenderer.widthMultiplier;
        autodestruct = TrailRenderer.autodestruct;
        emitting = TrailRenderer.emitting;
        numCornerVertices = TrailRenderer.numCornerVertices;
        numCapVertices = TrailRenderer.numCapVertices;
        minVertexDistance = TrailRenderer.minVertexDistance;
        startColor = TrailRenderer.startColor;
        endColor = TrailRenderer.endColor;
        shadowBias = TrailRenderer.shadowBias;
        generateLightingData = TrailRenderer.generateLightingData;
        textureMode = TrailRenderer.textureMode;
        alignment = TrailRenderer.alignment;
        widthCurve = TrailRenderer.widthCurve;
        colorGradient = TrailRenderer.colorGradient;
        enabled = TrailRenderer.enabled;
        shadowCastingMode = TrailRenderer.shadowCastingMode;
        receiveShadows = TrailRenderer.receiveShadows;
        forceRenderingOff = TrailRenderer.forceRenderingOff;
        motionVectorGenerationMode = TrailRenderer.motionVectorGenerationMode;
        lightProbeUsage = TrailRenderer.lightProbeUsage;
        reflectionProbeUsage = TrailRenderer.reflectionProbeUsage;
        renderingLayerMask = TrailRenderer.renderingLayerMask;
        rendererPriority = TrailRenderer.rendererPriority;
        rayTracingMode = TrailRenderer.rayTracingMode;
        sortingLayerName = TrailRenderer.sortingLayerName;
        sortingLayerID = TrailRenderer.sortingLayerID;
        sortingOrder = TrailRenderer.sortingOrder;
        allowOcclusionWhenDynamic = TrailRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = TrailRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = TrailRenderer.probeAnchor;
        lightmapIndex = TrailRenderer.lightmapIndex;
        realtimeLightmapIndex = TrailRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = TrailRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = TrailRenderer.realtimeLightmapScaleOffset;
        materials = TrailRenderer.materials;
        material = TrailRenderer.material;
        sharedMaterial = TrailRenderer.sharedMaterial;
        sharedMaterials = TrailRenderer.sharedMaterials;
        tag = TrailRenderer.tag;
        name = TrailRenderer.name;
        hideFlags = TrailRenderer.hideFlags;
    }
}
[System.Serializable]
public class LineRendererZSaver : ZSave.ZSaver<UnityEngine.LineRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LineRendererZSaver (UnityEngine.LineRenderer LineRenderer) : base(LineRenderer.gameObject, LineRenderer) {
        startWidth = LineRenderer.startWidth;
        endWidth = LineRenderer.endWidth;
        widthMultiplier = LineRenderer.widthMultiplier;
        numCornerVertices = LineRenderer.numCornerVertices;
        numCapVertices = LineRenderer.numCapVertices;
        useWorldSpace = LineRenderer.useWorldSpace;
        loop = LineRenderer.loop;
        startColor = LineRenderer.startColor;
        endColor = LineRenderer.endColor;
        positionCount = LineRenderer.positionCount;
        shadowBias = LineRenderer.shadowBias;
        generateLightingData = LineRenderer.generateLightingData;
        textureMode = LineRenderer.textureMode;
        alignment = LineRenderer.alignment;
        widthCurve = LineRenderer.widthCurve;
        colorGradient = LineRenderer.colorGradient;
        enabled = LineRenderer.enabled;
        shadowCastingMode = LineRenderer.shadowCastingMode;
        receiveShadows = LineRenderer.receiveShadows;
        forceRenderingOff = LineRenderer.forceRenderingOff;
        motionVectorGenerationMode = LineRenderer.motionVectorGenerationMode;
        lightProbeUsage = LineRenderer.lightProbeUsage;
        reflectionProbeUsage = LineRenderer.reflectionProbeUsage;
        renderingLayerMask = LineRenderer.renderingLayerMask;
        rendererPriority = LineRenderer.rendererPriority;
        rayTracingMode = LineRenderer.rayTracingMode;
        sortingLayerName = LineRenderer.sortingLayerName;
        sortingLayerID = LineRenderer.sortingLayerID;
        sortingOrder = LineRenderer.sortingOrder;
        allowOcclusionWhenDynamic = LineRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = LineRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = LineRenderer.probeAnchor;
        lightmapIndex = LineRenderer.lightmapIndex;
        realtimeLightmapIndex = LineRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = LineRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = LineRenderer.realtimeLightmapScaleOffset;
        materials = LineRenderer.materials;
        material = LineRenderer.material;
        sharedMaterial = LineRenderer.sharedMaterial;
        sharedMaterials = LineRenderer.sharedMaterials;
        tag = LineRenderer.tag;
        name = LineRenderer.name;
        hideFlags = LineRenderer.hideFlags;
    }
}
[System.Serializable]
public class OcclusionPortalZSaver : ZSave.ZSaver<UnityEngine.OcclusionPortal> {
    public System.Boolean open;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public OcclusionPortalZSaver (UnityEngine.OcclusionPortal OcclusionPortal) : base(OcclusionPortal.gameObject, OcclusionPortal) {
        open = OcclusionPortal.open;
        tag = OcclusionPortal.tag;
        name = OcclusionPortal.name;
        hideFlags = OcclusionPortal.hideFlags;
    }
}
[System.Serializable]
public class OcclusionAreaZSaver : ZSave.ZSaver<UnityEngine.OcclusionArea> {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public OcclusionAreaZSaver (UnityEngine.OcclusionArea OcclusionArea) : base(OcclusionArea.gameObject, OcclusionArea) {
        center = OcclusionArea.center;
        size = OcclusionArea.size;
        tag = OcclusionArea.tag;
        name = OcclusionArea.name;
        hideFlags = OcclusionArea.hideFlags;
    }
}
[System.Serializable]
public class LensFlareZSaver : ZSave.ZSaver<UnityEngine.LensFlare> {
    public System.Single brightness;
    public System.Single fadeSpeed;
    public UnityEngine.Color color;
    public UnityEngine.Flare flare;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LensFlareZSaver (UnityEngine.LensFlare LensFlare) : base(LensFlare.gameObject, LensFlare) {
        brightness = LensFlare.brightness;
        fadeSpeed = LensFlare.fadeSpeed;
        color = LensFlare.color;
        flare = LensFlare.flare;
        enabled = LensFlare.enabled;
        tag = LensFlare.tag;
        name = LensFlare.name;
        hideFlags = LensFlare.hideFlags;
    }
}
[System.Serializable]
public class LightZSaver : ZSave.ZSaver<UnityEngine.Light> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LightZSaver (UnityEngine.Light Light) : base(Light.gameObject, Light) {
        type = Light.type;
        shape = Light.shape;
        spotAngle = Light.spotAngle;
        innerSpotAngle = Light.innerSpotAngle;
        color = Light.color;
        colorTemperature = Light.colorTemperature;
        useColorTemperature = Light.useColorTemperature;
        intensity = Light.intensity;
        bounceIntensity = Light.bounceIntensity;
        useBoundingSphereOverride = Light.useBoundingSphereOverride;
        boundingSphereOverride = Light.boundingSphereOverride;
        useViewFrustumForShadowCasterCull = Light.useViewFrustumForShadowCasterCull;
        shadowCustomResolution = Light.shadowCustomResolution;
        shadowBias = Light.shadowBias;
        shadowNormalBias = Light.shadowNormalBias;
        shadowNearPlane = Light.shadowNearPlane;
        useShadowMatrixOverride = Light.useShadowMatrixOverride;
        shadowMatrixOverride = Light.shadowMatrixOverride;
        range = Light.range;
        flare = Light.flare;
        bakingOutput = Light.bakingOutput;
        cullingMask = Light.cullingMask;
        renderingLayerMask = Light.renderingLayerMask;
        lightShadowCasterMode = Light.lightShadowCasterMode;
        shadows = Light.shadows;
        shadowStrength = Light.shadowStrength;
        shadowResolution = Light.shadowResolution;
        layerShadowCullDistances = Light.layerShadowCullDistances;
        cookieSize = Light.cookieSize;
        cookie = Light.cookie;
        renderMode = Light.renderMode;
        enabled = Light.enabled;
        tag = Light.tag;
        name = Light.name;
        hideFlags = Light.hideFlags;
    }
}
[System.Serializable]
public class SkyboxZSaver : ZSave.ZSaver<UnityEngine.Skybox> {
    public UnityEngine.Material material;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SkyboxZSaver (UnityEngine.Skybox Skybox) : base(Skybox.gameObject, Skybox) {
        material = Skybox.material;
        enabled = Skybox.enabled;
        tag = Skybox.tag;
        name = Skybox.name;
        hideFlags = Skybox.hideFlags;
    }
}
[System.Serializable]
public class MeshFilterZSaver : ZSave.ZSaver<UnityEngine.MeshFilter> {
    public UnityEngine.Mesh sharedMesh;
    public UnityEngine.Mesh mesh;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public MeshFilterZSaver (UnityEngine.MeshFilter MeshFilter) : base(MeshFilter.gameObject, MeshFilter) {
        sharedMesh = MeshFilter.sharedMesh;
        mesh = MeshFilter.mesh;
        tag = MeshFilter.tag;
        name = MeshFilter.name;
        hideFlags = MeshFilter.hideFlags;
    }
}
[System.Serializable]
public class LightProbeProxyVolumeZSaver : ZSave.ZSaver<UnityEngine.LightProbeProxyVolume> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LightProbeProxyVolumeZSaver (UnityEngine.LightProbeProxyVolume LightProbeProxyVolume) : base(LightProbeProxyVolume.gameObject, LightProbeProxyVolume) {
        sizeCustom = LightProbeProxyVolume.sizeCustom;
        originCustom = LightProbeProxyVolume.originCustom;
        probeDensity = LightProbeProxyVolume.probeDensity;
        gridResolutionX = LightProbeProxyVolume.gridResolutionX;
        gridResolutionY = LightProbeProxyVolume.gridResolutionY;
        gridResolutionZ = LightProbeProxyVolume.gridResolutionZ;
        boundingBoxMode = LightProbeProxyVolume.boundingBoxMode;
        resolutionMode = LightProbeProxyVolume.resolutionMode;
        probePositionMode = LightProbeProxyVolume.probePositionMode;
        refreshMode = LightProbeProxyVolume.refreshMode;
        qualityMode = LightProbeProxyVolume.qualityMode;
        dataFormat = LightProbeProxyVolume.dataFormat;
        enabled = LightProbeProxyVolume.enabled;
        tag = LightProbeProxyVolume.tag;
        name = LightProbeProxyVolume.name;
        hideFlags = LightProbeProxyVolume.hideFlags;
    }
}
[System.Serializable]
public class SkinnedMeshRendererZSaver : ZSave.ZSaver<UnityEngine.SkinnedMeshRenderer> {
    public UnityEngine.SkinQuality quality;
    public System.Boolean updateWhenOffscreen;
    public System.Boolean forceMatrixRecalculationPerRender;
    public UnityEngine.Transform rootBone;
    public UnityEngine.Transform[] bones;
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean skinnedMotionVectors;
    public UnityEngine.Bounds localBounds;
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SkinnedMeshRendererZSaver (UnityEngine.SkinnedMeshRenderer SkinnedMeshRenderer) : base(SkinnedMeshRenderer.gameObject, SkinnedMeshRenderer) {
        quality = SkinnedMeshRenderer.quality;
        updateWhenOffscreen = SkinnedMeshRenderer.updateWhenOffscreen;
        forceMatrixRecalculationPerRender = SkinnedMeshRenderer.forceMatrixRecalculationPerRender;
        rootBone = SkinnedMeshRenderer.rootBone;
        bones = SkinnedMeshRenderer.bones;
        sharedMesh = SkinnedMeshRenderer.sharedMesh;
        skinnedMotionVectors = SkinnedMeshRenderer.skinnedMotionVectors;
        localBounds = SkinnedMeshRenderer.localBounds;
        enabled = SkinnedMeshRenderer.enabled;
        shadowCastingMode = SkinnedMeshRenderer.shadowCastingMode;
        receiveShadows = SkinnedMeshRenderer.receiveShadows;
        forceRenderingOff = SkinnedMeshRenderer.forceRenderingOff;
        motionVectorGenerationMode = SkinnedMeshRenderer.motionVectorGenerationMode;
        lightProbeUsage = SkinnedMeshRenderer.lightProbeUsage;
        reflectionProbeUsage = SkinnedMeshRenderer.reflectionProbeUsage;
        renderingLayerMask = SkinnedMeshRenderer.renderingLayerMask;
        rendererPriority = SkinnedMeshRenderer.rendererPriority;
        rayTracingMode = SkinnedMeshRenderer.rayTracingMode;
        sortingLayerName = SkinnedMeshRenderer.sortingLayerName;
        sortingLayerID = SkinnedMeshRenderer.sortingLayerID;
        sortingOrder = SkinnedMeshRenderer.sortingOrder;
        allowOcclusionWhenDynamic = SkinnedMeshRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SkinnedMeshRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = SkinnedMeshRenderer.probeAnchor;
        lightmapIndex = SkinnedMeshRenderer.lightmapIndex;
        realtimeLightmapIndex = SkinnedMeshRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = SkinnedMeshRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SkinnedMeshRenderer.realtimeLightmapScaleOffset;
        materials = SkinnedMeshRenderer.materials;
        material = SkinnedMeshRenderer.material;
        sharedMaterial = SkinnedMeshRenderer.sharedMaterial;
        sharedMaterials = SkinnedMeshRenderer.sharedMaterials;
        tag = SkinnedMeshRenderer.tag;
        name = SkinnedMeshRenderer.name;
        hideFlags = SkinnedMeshRenderer.hideFlags;
    }
}
[System.Serializable]
public class MeshRendererZSaver : ZSave.ZSaver<UnityEngine.MeshRenderer> {
    public UnityEngine.Mesh additionalVertexStreams;
    public UnityEngine.Mesh enlightenVertexStream;
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public MeshRendererZSaver (UnityEngine.MeshRenderer MeshRenderer) : base(MeshRenderer.gameObject, MeshRenderer) {
        additionalVertexStreams = MeshRenderer.additionalVertexStreams;
        enlightenVertexStream = MeshRenderer.enlightenVertexStream;
        enabled = MeshRenderer.enabled;
        shadowCastingMode = MeshRenderer.shadowCastingMode;
        receiveShadows = MeshRenderer.receiveShadows;
        forceRenderingOff = MeshRenderer.forceRenderingOff;
        motionVectorGenerationMode = MeshRenderer.motionVectorGenerationMode;
        lightProbeUsage = MeshRenderer.lightProbeUsage;
        reflectionProbeUsage = MeshRenderer.reflectionProbeUsage;
        renderingLayerMask = MeshRenderer.renderingLayerMask;
        rendererPriority = MeshRenderer.rendererPriority;
        rayTracingMode = MeshRenderer.rayTracingMode;
        sortingLayerName = MeshRenderer.sortingLayerName;
        sortingLayerID = MeshRenderer.sortingLayerID;
        sortingOrder = MeshRenderer.sortingOrder;
        allowOcclusionWhenDynamic = MeshRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = MeshRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = MeshRenderer.probeAnchor;
        lightmapIndex = MeshRenderer.lightmapIndex;
        realtimeLightmapIndex = MeshRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = MeshRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = MeshRenderer.realtimeLightmapScaleOffset;
        materials = MeshRenderer.materials;
        material = MeshRenderer.material;
        sharedMaterial = MeshRenderer.sharedMaterial;
        sharedMaterials = MeshRenderer.sharedMaterials;
        tag = MeshRenderer.tag;
        name = MeshRenderer.name;
        hideFlags = MeshRenderer.hideFlags;
    }
}
[System.Serializable]
public class LightProbeGroupZSaver : ZSave.ZSaver<UnityEngine.LightProbeGroup> {
    public UnityEngine.Vector3[] probePositions;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LightProbeGroupZSaver (UnityEngine.LightProbeGroup LightProbeGroup) : base(LightProbeGroup.gameObject, LightProbeGroup) {
        probePositions = LightProbeGroup.probePositions;
        enabled = LightProbeGroup.enabled;
        tag = LightProbeGroup.tag;
        name = LightProbeGroup.name;
        hideFlags = LightProbeGroup.hideFlags;
    }
}
[System.Serializable]
public class LODGroupZSaver : ZSave.ZSaver<UnityEngine.LODGroup> {
    public UnityEngine.Vector3 localReferencePoint;
    public System.Single size;
    public UnityEngine.LODFadeMode fadeMode;
    public System.Boolean animateCrossFading;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public LODGroupZSaver (UnityEngine.LODGroup LODGroup) : base(LODGroup.gameObject, LODGroup) {
        localReferencePoint = LODGroup.localReferencePoint;
        size = LODGroup.size;
        fadeMode = LODGroup.fadeMode;
        animateCrossFading = LODGroup.animateCrossFading;
        enabled = LODGroup.enabled;
        tag = LODGroup.tag;
        name = LODGroup.name;
        hideFlags = LODGroup.hideFlags;
    }
}
[System.Serializable]
public class BehaviourZSaver : ZSave.ZSaver<UnityEngine.Behaviour> {
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public BehaviourZSaver (UnityEngine.Behaviour Behaviour) : base(Behaviour.gameObject, Behaviour) {
        enabled = Behaviour.enabled;
        tag = Behaviour.tag;
        name = Behaviour.name;
        hideFlags = Behaviour.hideFlags;
    }
}
[System.Serializable]
public class RectTransformZSaver : ZSave.ZSaver<UnityEngine.RectTransform> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public RectTransformZSaver (UnityEngine.RectTransform RectTransform) : base(RectTransform.gameObject, RectTransform) {
        anchorMin = RectTransform.anchorMin;
        anchorMax = RectTransform.anchorMax;
        anchoredPosition = RectTransform.anchoredPosition;
        sizeDelta = RectTransform.sizeDelta;
        pivot = RectTransform.pivot;
        anchoredPosition3D = RectTransform.anchoredPosition3D;
        offsetMin = RectTransform.offsetMin;
        offsetMax = RectTransform.offsetMax;
        position = RectTransform.position;
        localPosition = RectTransform.localPosition;
        eulerAngles = RectTransform.eulerAngles;
        localEulerAngles = RectTransform.localEulerAngles;
        right = RectTransform.right;
        up = RectTransform.up;
        forward = RectTransform.forward;
        rotation = RectTransform.rotation;
        localRotation = RectTransform.localRotation;
        localScale = RectTransform.localScale;
        parent = RectTransform.parent;
        hasChanged = RectTransform.hasChanged;
        hierarchyCapacity = RectTransform.hierarchyCapacity;
        tag = RectTransform.tag;
        name = RectTransform.name;
        hideFlags = RectTransform.hideFlags;
    }
}
[System.Serializable]
public class SpriteRendererZSaver : ZSave.ZSaver<UnityEngine.SpriteRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SpriteRendererZSaver (UnityEngine.SpriteRenderer SpriteRenderer) : base(SpriteRenderer.gameObject, SpriteRenderer) {
        sprite = SpriteRenderer.sprite;
        drawMode = SpriteRenderer.drawMode;
        size = SpriteRenderer.size;
        adaptiveModeThreshold = SpriteRenderer.adaptiveModeThreshold;
        tileMode = SpriteRenderer.tileMode;
        color = SpriteRenderer.color;
        maskInteraction = SpriteRenderer.maskInteraction;
        flipX = SpriteRenderer.flipX;
        flipY = SpriteRenderer.flipY;
        spriteSortPoint = SpriteRenderer.spriteSortPoint;
        enabled = SpriteRenderer.enabled;
        shadowCastingMode = SpriteRenderer.shadowCastingMode;
        receiveShadows = SpriteRenderer.receiveShadows;
        forceRenderingOff = SpriteRenderer.forceRenderingOff;
        motionVectorGenerationMode = SpriteRenderer.motionVectorGenerationMode;
        lightProbeUsage = SpriteRenderer.lightProbeUsage;
        reflectionProbeUsage = SpriteRenderer.reflectionProbeUsage;
        renderingLayerMask = SpriteRenderer.renderingLayerMask;
        rendererPriority = SpriteRenderer.rendererPriority;
        rayTracingMode = SpriteRenderer.rayTracingMode;
        sortingLayerName = SpriteRenderer.sortingLayerName;
        sortingLayerID = SpriteRenderer.sortingLayerID;
        sortingOrder = SpriteRenderer.sortingOrder;
        allowOcclusionWhenDynamic = SpriteRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SpriteRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = SpriteRenderer.probeAnchor;
        lightmapIndex = SpriteRenderer.lightmapIndex;
        realtimeLightmapIndex = SpriteRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = SpriteRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SpriteRenderer.realtimeLightmapScaleOffset;
        materials = SpriteRenderer.materials;
        material = SpriteRenderer.material;
        sharedMaterial = SpriteRenderer.sharedMaterial;
        sharedMaterials = SpriteRenderer.sharedMaterials;
        tag = SpriteRenderer.tag;
        name = SpriteRenderer.name;
        hideFlags = SpriteRenderer.hideFlags;
    }
}
[System.Serializable]
public class SortingGroupZSaver : ZSave.ZSaver<UnityEngine.Rendering.SortingGroup> {
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SortingGroupZSaver (UnityEngine.Rendering.SortingGroup SortingGroup) : base(SortingGroup.gameObject, SortingGroup) {
        sortingLayerName = SortingGroup.sortingLayerName;
        sortingLayerID = SortingGroup.sortingLayerID;
        sortingOrder = SortingGroup.sortingOrder;
        enabled = SortingGroup.enabled;
        tag = SortingGroup.tag;
        name = SortingGroup.name;
        hideFlags = SortingGroup.hideFlags;
    }
}
[System.Serializable]
public class PlayableDirectorZSaver : ZSave.ZSaver<UnityEngine.Playables.PlayableDirector> {
    public UnityEngine.Playables.DirectorWrapMode extrapolationMode;
    public UnityEngine.Playables.PlayableAsset playableAsset;
    public System.Boolean playOnAwake;
    public UnityEngine.Playables.DirectorUpdateMode timeUpdateMode;
    public System.Double time;
    public System.Double initialTime;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PlayableDirectorZSaver (UnityEngine.Playables.PlayableDirector PlayableDirector) : base(PlayableDirector.gameObject, PlayableDirector) {
        extrapolationMode = PlayableDirector.extrapolationMode;
        playableAsset = PlayableDirector.playableAsset;
        playOnAwake = PlayableDirector.playOnAwake;
        timeUpdateMode = PlayableDirector.timeUpdateMode;
        time = PlayableDirector.time;
        initialTime = PlayableDirector.initialTime;
        enabled = PlayableDirector.enabled;
        tag = PlayableDirector.tag;
        name = PlayableDirector.name;
        hideFlags = PlayableDirector.hideFlags;
    }
}
[System.Serializable]
public class GridZSaver : ZSave.ZSaver<UnityEngine.Grid> {
    public UnityEngine.Vector3 cellSize;
    public UnityEngine.Vector3 cellGap;
    public UnityEngine.GridLayout.CellLayout cellLayout;
    public UnityEngine.GridLayout.CellSwizzle cellSwizzle;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public GridZSaver (UnityEngine.Grid Grid) : base(Grid.gameObject, Grid) {
        cellSize = Grid.cellSize;
        cellGap = Grid.cellGap;
        cellLayout = Grid.cellLayout;
        cellSwizzle = Grid.cellSwizzle;
        enabled = Grid.enabled;
        tag = Grid.tag;
        name = Grid.name;
        hideFlags = Grid.hideFlags;
    }
}
[System.Serializable]
public class GridLayoutZSaver : ZSave.ZSaver<UnityEngine.GridLayout> {
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public GridLayoutZSaver (UnityEngine.GridLayout GridLayout) : base(GridLayout.gameObject, GridLayout) {
        enabled = GridLayout.enabled;
        tag = GridLayout.tag;
        name = GridLayout.name;
        hideFlags = GridLayout.hideFlags;
    }
}
[System.Serializable]
public class ParticleSystemZSaver : ZSave.ZSaver<UnityEngine.ParticleSystem> {
    public System.Single time;
    public System.UInt32 randomSeed;
    public System.Boolean useAutoRandomSeed;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemZSaver (UnityEngine.ParticleSystem ParticleSystem) : base(ParticleSystem.gameObject, ParticleSystem) {
        time = ParticleSystem.time;
        randomSeed = ParticleSystem.randomSeed;
        useAutoRandomSeed = ParticleSystem.useAutoRandomSeed;
        tag = ParticleSystem.tag;
        name = ParticleSystem.name;
        hideFlags = ParticleSystem.hideFlags;
    }
}
[System.Serializable]
public class ParticleSystemRendererZSaver : ZSave.ZSaver<UnityEngine.ParticleSystemRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemRendererZSaver (UnityEngine.ParticleSystemRenderer ParticleSystemRenderer) : base(ParticleSystemRenderer.gameObject, ParticleSystemRenderer) {
        alignment = ParticleSystemRenderer.alignment;
        renderMode = ParticleSystemRenderer.renderMode;
        sortMode = ParticleSystemRenderer.sortMode;
        lengthScale = ParticleSystemRenderer.lengthScale;
        velocityScale = ParticleSystemRenderer.velocityScale;
        cameraVelocityScale = ParticleSystemRenderer.cameraVelocityScale;
        normalDirection = ParticleSystemRenderer.normalDirection;
        shadowBias = ParticleSystemRenderer.shadowBias;
        sortingFudge = ParticleSystemRenderer.sortingFudge;
        minParticleSize = ParticleSystemRenderer.minParticleSize;
        maxParticleSize = ParticleSystemRenderer.maxParticleSize;
        pivot = ParticleSystemRenderer.pivot;
        flip = ParticleSystemRenderer.flip;
        maskInteraction = ParticleSystemRenderer.maskInteraction;
        trailMaterial = ParticleSystemRenderer.trailMaterial;
        enableGPUInstancing = ParticleSystemRenderer.enableGPUInstancing;
        allowRoll = ParticleSystemRenderer.allowRoll;
        freeformStretching = ParticleSystemRenderer.freeformStretching;
        rotateWithStretchDirection = ParticleSystemRenderer.rotateWithStretchDirection;
        mesh = ParticleSystemRenderer.mesh;
        enabled = ParticleSystemRenderer.enabled;
        shadowCastingMode = ParticleSystemRenderer.shadowCastingMode;
        receiveShadows = ParticleSystemRenderer.receiveShadows;
        forceRenderingOff = ParticleSystemRenderer.forceRenderingOff;
        motionVectorGenerationMode = ParticleSystemRenderer.motionVectorGenerationMode;
        lightProbeUsage = ParticleSystemRenderer.lightProbeUsage;
        reflectionProbeUsage = ParticleSystemRenderer.reflectionProbeUsage;
        renderingLayerMask = ParticleSystemRenderer.renderingLayerMask;
        rendererPriority = ParticleSystemRenderer.rendererPriority;
        rayTracingMode = ParticleSystemRenderer.rayTracingMode;
        sortingLayerName = ParticleSystemRenderer.sortingLayerName;
        sortingLayerID = ParticleSystemRenderer.sortingLayerID;
        sortingOrder = ParticleSystemRenderer.sortingOrder;
        allowOcclusionWhenDynamic = ParticleSystemRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = ParticleSystemRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = ParticleSystemRenderer.probeAnchor;
        lightmapIndex = ParticleSystemRenderer.lightmapIndex;
        realtimeLightmapIndex = ParticleSystemRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = ParticleSystemRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = ParticleSystemRenderer.realtimeLightmapScaleOffset;
        materials = ParticleSystemRenderer.materials;
        material = ParticleSystemRenderer.material;
        sharedMaterial = ParticleSystemRenderer.sharedMaterial;
        sharedMaterials = ParticleSystemRenderer.sharedMaterials;
        tag = ParticleSystemRenderer.tag;
        name = ParticleSystemRenderer.name;
        hideFlags = ParticleSystemRenderer.hideFlags;
    }
}
[System.Serializable]
public class ParticleSystemForceFieldZSaver : ZSave.ZSaver<UnityEngine.ParticleSystemForceField> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ParticleSystemForceFieldZSaver (UnityEngine.ParticleSystemForceField ParticleSystemForceField) : base(ParticleSystemForceField.gameObject, ParticleSystemForceField) {
        shape = ParticleSystemForceField.shape;
        startRange = ParticleSystemForceField.startRange;
        endRange = ParticleSystemForceField.endRange;
        length = ParticleSystemForceField.length;
        gravityFocus = ParticleSystemForceField.gravityFocus;
        rotationRandomness = ParticleSystemForceField.rotationRandomness;
        multiplyDragByParticleSize = ParticleSystemForceField.multiplyDragByParticleSize;
        multiplyDragByParticleVelocity = ParticleSystemForceField.multiplyDragByParticleVelocity;
        vectorField = ParticleSystemForceField.vectorField;
        directionX = ParticleSystemForceField.directionX;
        directionY = ParticleSystemForceField.directionY;
        directionZ = ParticleSystemForceField.directionZ;
        gravity = ParticleSystemForceField.gravity;
        rotationSpeed = ParticleSystemForceField.rotationSpeed;
        rotationAttraction = ParticleSystemForceField.rotationAttraction;
        drag = ParticleSystemForceField.drag;
        vectorFieldSpeed = ParticleSystemForceField.vectorFieldSpeed;
        vectorFieldAttraction = ParticleSystemForceField.vectorFieldAttraction;
        tag = ParticleSystemForceField.tag;
        name = ParticleSystemForceField.name;
        hideFlags = ParticleSystemForceField.hideFlags;
    }
}
[System.Serializable]
public class RigidbodyZSaver : ZSave.ZSaver<UnityEngine.Rigidbody> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public RigidbodyZSaver (UnityEngine.Rigidbody Rigidbody) : base(Rigidbody.gameObject, Rigidbody) {
        velocity = Rigidbody.velocity;
        angularVelocity = Rigidbody.angularVelocity;
        drag = Rigidbody.drag;
        angularDrag = Rigidbody.angularDrag;
        mass = Rigidbody.mass;
        useGravity = Rigidbody.useGravity;
        maxDepenetrationVelocity = Rigidbody.maxDepenetrationVelocity;
        isKinematic = Rigidbody.isKinematic;
        freezeRotation = Rigidbody.freezeRotation;
        constraints = Rigidbody.constraints;
        collisionDetectionMode = Rigidbody.collisionDetectionMode;
        centerOfMass = Rigidbody.centerOfMass;
        inertiaTensorRotation = Rigidbody.inertiaTensorRotation;
        inertiaTensor = Rigidbody.inertiaTensor;
        detectCollisions = Rigidbody.detectCollisions;
        position = Rigidbody.position;
        rotation = Rigidbody.rotation;
        interpolation = Rigidbody.interpolation;
        solverIterations = Rigidbody.solverIterations;
        sleepThreshold = Rigidbody.sleepThreshold;
        maxAngularVelocity = Rigidbody.maxAngularVelocity;
        solverVelocityIterations = Rigidbody.solverVelocityIterations;
        tag = Rigidbody.tag;
        name = Rigidbody.name;
        hideFlags = Rigidbody.hideFlags;
    }
}
[System.Serializable]
public class ColliderZSaver : ZSave.ZSaver<UnityEngine.Collider> {
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ColliderZSaver (UnityEngine.Collider Collider) : base(Collider.gameObject, Collider) {
        enabled = Collider.enabled;
        isTrigger = Collider.isTrigger;
        contactOffset = Collider.contactOffset;
        sharedMaterial = Collider.sharedMaterial;
        material = Collider.material;
        tag = Collider.tag;
        name = Collider.name;
        hideFlags = Collider.hideFlags;
    }
}
[System.Serializable]
public class CharacterControllerZSaver : ZSave.ZSaver<UnityEngine.CharacterController> {
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
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CharacterControllerZSaver (UnityEngine.CharacterController CharacterController) : base(CharacterController.gameObject, CharacterController) {
        radius = CharacterController.radius;
        height = CharacterController.height;
        center = CharacterController.center;
        slopeLimit = CharacterController.slopeLimit;
        stepOffset = CharacterController.stepOffset;
        skinWidth = CharacterController.skinWidth;
        minMoveDistance = CharacterController.minMoveDistance;
        detectCollisions = CharacterController.detectCollisions;
        enableOverlapRecovery = CharacterController.enableOverlapRecovery;
        enabled = CharacterController.enabled;
        isTrigger = CharacterController.isTrigger;
        contactOffset = CharacterController.contactOffset;
        sharedMaterial = CharacterController.sharedMaterial;
        material = CharacterController.material;
        tag = CharacterController.tag;
        name = CharacterController.name;
        hideFlags = CharacterController.hideFlags;
    }
}
[System.Serializable]
public class MeshColliderZSaver : ZSave.ZSaver<UnityEngine.MeshCollider> {
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean convex;
    public UnityEngine.MeshColliderCookingOptions cookingOptions;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public MeshColliderZSaver (UnityEngine.MeshCollider MeshCollider) : base(MeshCollider.gameObject, MeshCollider) {
        sharedMesh = MeshCollider.sharedMesh;
        convex = MeshCollider.convex;
        cookingOptions = MeshCollider.cookingOptions;
        enabled = MeshCollider.enabled;
        isTrigger = MeshCollider.isTrigger;
        contactOffset = MeshCollider.contactOffset;
        sharedMaterial = MeshCollider.sharedMaterial;
        material = MeshCollider.material;
        tag = MeshCollider.tag;
        name = MeshCollider.name;
        hideFlags = MeshCollider.hideFlags;
    }
}
[System.Serializable]
public class CapsuleColliderZSaver : ZSave.ZSaver<UnityEngine.CapsuleCollider> {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Single height;
    public System.Int32 direction;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CapsuleColliderZSaver (UnityEngine.CapsuleCollider CapsuleCollider) : base(CapsuleCollider.gameObject, CapsuleCollider) {
        center = CapsuleCollider.center;
        radius = CapsuleCollider.radius;
        height = CapsuleCollider.height;
        direction = CapsuleCollider.direction;
        enabled = CapsuleCollider.enabled;
        isTrigger = CapsuleCollider.isTrigger;
        contactOffset = CapsuleCollider.contactOffset;
        sharedMaterial = CapsuleCollider.sharedMaterial;
        material = CapsuleCollider.material;
        tag = CapsuleCollider.tag;
        name = CapsuleCollider.name;
        hideFlags = CapsuleCollider.hideFlags;
    }
}
[System.Serializable]
public class BoxColliderZSaver : ZSave.ZSaver<UnityEngine.BoxCollider> {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public BoxColliderZSaver (UnityEngine.BoxCollider BoxCollider) : base(BoxCollider.gameObject, BoxCollider) {
        center = BoxCollider.center;
        size = BoxCollider.size;
        enabled = BoxCollider.enabled;
        isTrigger = BoxCollider.isTrigger;
        contactOffset = BoxCollider.contactOffset;
        sharedMaterial = BoxCollider.sharedMaterial;
        material = BoxCollider.material;
        tag = BoxCollider.tag;
        name = BoxCollider.name;
        hideFlags = BoxCollider.hideFlags;
    }
}
[System.Serializable]
public class SphereColliderZSaver : ZSave.ZSaver<UnityEngine.SphereCollider> {
    public UnityEngine.Vector3 center;
    public System.Single radius;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SphereColliderZSaver (UnityEngine.SphereCollider SphereCollider) : base(SphereCollider.gameObject, SphereCollider) {
        center = SphereCollider.center;
        radius = SphereCollider.radius;
        enabled = SphereCollider.enabled;
        isTrigger = SphereCollider.isTrigger;
        contactOffset = SphereCollider.contactOffset;
        sharedMaterial = SphereCollider.sharedMaterial;
        material = SphereCollider.material;
        tag = SphereCollider.tag;
        name = SphereCollider.name;
        hideFlags = SphereCollider.hideFlags;
    }
}
[System.Serializable]
public class ConstantForceZSaver : ZSave.ZSaver<UnityEngine.ConstantForce> {
    public UnityEngine.Vector3 force;
    public UnityEngine.Vector3 relativeForce;
    public UnityEngine.Vector3 torque;
    public UnityEngine.Vector3 relativeTorque;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ConstantForceZSaver (UnityEngine.ConstantForce ConstantForce) : base(ConstantForce.gameObject, ConstantForce) {
        force = ConstantForce.force;
        relativeForce = ConstantForce.relativeForce;
        torque = ConstantForce.torque;
        relativeTorque = ConstantForce.relativeTorque;
        enabled = ConstantForce.enabled;
        tag = ConstantForce.tag;
        name = ConstantForce.name;
        hideFlags = ConstantForce.hideFlags;
    }
}
[System.Serializable]
public class JointZSaver : ZSave.ZSaver<UnityEngine.Joint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public JointZSaver (UnityEngine.Joint Joint) : base(Joint.gameObject, Joint) {
        connectedBody = Joint.connectedBody;
        connectedArticulationBody = Joint.connectedArticulationBody;
        axis = Joint.axis;
        anchor = Joint.anchor;
        connectedAnchor = Joint.connectedAnchor;
        autoConfigureConnectedAnchor = Joint.autoConfigureConnectedAnchor;
        breakForce = Joint.breakForce;
        breakTorque = Joint.breakTorque;
        enableCollision = Joint.enableCollision;
        enablePreprocessing = Joint.enablePreprocessing;
        massScale = Joint.massScale;
        connectedMassScale = Joint.connectedMassScale;
        tag = Joint.tag;
        name = Joint.name;
        hideFlags = Joint.hideFlags;
    }
}
[System.Serializable]
public class HingeJointZSaver : ZSave.ZSaver<UnityEngine.HingeJoint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public HingeJointZSaver (UnityEngine.HingeJoint HingeJoint) : base(HingeJoint.gameObject, HingeJoint) {
        motor = HingeJoint.motor;
        limits = HingeJoint.limits;
        spring = HingeJoint.spring;
        useMotor = HingeJoint.useMotor;
        useLimits = HingeJoint.useLimits;
        useSpring = HingeJoint.useSpring;
        connectedBody = HingeJoint.connectedBody;
        connectedArticulationBody = HingeJoint.connectedArticulationBody;
        axis = HingeJoint.axis;
        anchor = HingeJoint.anchor;
        connectedAnchor = HingeJoint.connectedAnchor;
        autoConfigureConnectedAnchor = HingeJoint.autoConfigureConnectedAnchor;
        breakForce = HingeJoint.breakForce;
        breakTorque = HingeJoint.breakTorque;
        enableCollision = HingeJoint.enableCollision;
        enablePreprocessing = HingeJoint.enablePreprocessing;
        massScale = HingeJoint.massScale;
        connectedMassScale = HingeJoint.connectedMassScale;
        tag = HingeJoint.tag;
        name = HingeJoint.name;
        hideFlags = HingeJoint.hideFlags;
    }
}
[System.Serializable]
public class SpringJointZSaver : ZSave.ZSaver<UnityEngine.SpringJoint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SpringJointZSaver (UnityEngine.SpringJoint SpringJoint) : base(SpringJoint.gameObject, SpringJoint) {
        spring = SpringJoint.spring;
        damper = SpringJoint.damper;
        minDistance = SpringJoint.minDistance;
        maxDistance = SpringJoint.maxDistance;
        tolerance = SpringJoint.tolerance;
        connectedBody = SpringJoint.connectedBody;
        connectedArticulationBody = SpringJoint.connectedArticulationBody;
        axis = SpringJoint.axis;
        anchor = SpringJoint.anchor;
        connectedAnchor = SpringJoint.connectedAnchor;
        autoConfigureConnectedAnchor = SpringJoint.autoConfigureConnectedAnchor;
        breakForce = SpringJoint.breakForce;
        breakTorque = SpringJoint.breakTorque;
        enableCollision = SpringJoint.enableCollision;
        enablePreprocessing = SpringJoint.enablePreprocessing;
        massScale = SpringJoint.massScale;
        connectedMassScale = SpringJoint.connectedMassScale;
        tag = SpringJoint.tag;
        name = SpringJoint.name;
        hideFlags = SpringJoint.hideFlags;
    }
}
[System.Serializable]
public class FixedJointZSaver : ZSave.ZSaver<UnityEngine.FixedJoint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public FixedJointZSaver (UnityEngine.FixedJoint FixedJoint) : base(FixedJoint.gameObject, FixedJoint) {
        connectedBody = FixedJoint.connectedBody;
        connectedArticulationBody = FixedJoint.connectedArticulationBody;
        axis = FixedJoint.axis;
        anchor = FixedJoint.anchor;
        connectedAnchor = FixedJoint.connectedAnchor;
        autoConfigureConnectedAnchor = FixedJoint.autoConfigureConnectedAnchor;
        breakForce = FixedJoint.breakForce;
        breakTorque = FixedJoint.breakTorque;
        enableCollision = FixedJoint.enableCollision;
        enablePreprocessing = FixedJoint.enablePreprocessing;
        massScale = FixedJoint.massScale;
        connectedMassScale = FixedJoint.connectedMassScale;
        tag = FixedJoint.tag;
        name = FixedJoint.name;
        hideFlags = FixedJoint.hideFlags;
    }
}
[System.Serializable]
public class CharacterJointZSaver : ZSave.ZSaver<UnityEngine.CharacterJoint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CharacterJointZSaver (UnityEngine.CharacterJoint CharacterJoint) : base(CharacterJoint.gameObject, CharacterJoint) {
        swingAxis = CharacterJoint.swingAxis;
        twistLimitSpring = CharacterJoint.twistLimitSpring;
        swingLimitSpring = CharacterJoint.swingLimitSpring;
        lowTwistLimit = CharacterJoint.lowTwistLimit;
        highTwistLimit = CharacterJoint.highTwistLimit;
        swing1Limit = CharacterJoint.swing1Limit;
        swing2Limit = CharacterJoint.swing2Limit;
        enableProjection = CharacterJoint.enableProjection;
        projectionDistance = CharacterJoint.projectionDistance;
        projectionAngle = CharacterJoint.projectionAngle;
        connectedBody = CharacterJoint.connectedBody;
        connectedArticulationBody = CharacterJoint.connectedArticulationBody;
        axis = CharacterJoint.axis;
        anchor = CharacterJoint.anchor;
        connectedAnchor = CharacterJoint.connectedAnchor;
        autoConfigureConnectedAnchor = CharacterJoint.autoConfigureConnectedAnchor;
        breakForce = CharacterJoint.breakForce;
        breakTorque = CharacterJoint.breakTorque;
        enableCollision = CharacterJoint.enableCollision;
        enablePreprocessing = CharacterJoint.enablePreprocessing;
        massScale = CharacterJoint.massScale;
        connectedMassScale = CharacterJoint.connectedMassScale;
        tag = CharacterJoint.tag;
        name = CharacterJoint.name;
        hideFlags = CharacterJoint.hideFlags;
    }
}
[System.Serializable]
public class ConfigurableJointZSaver : ZSave.ZSaver<UnityEngine.ConfigurableJoint> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ConfigurableJointZSaver (UnityEngine.ConfigurableJoint ConfigurableJoint) : base(ConfigurableJoint.gameObject, ConfigurableJoint) {
        secondaryAxis = ConfigurableJoint.secondaryAxis;
        xMotion = ConfigurableJoint.xMotion;
        yMotion = ConfigurableJoint.yMotion;
        zMotion = ConfigurableJoint.zMotion;
        angularXMotion = ConfigurableJoint.angularXMotion;
        angularYMotion = ConfigurableJoint.angularYMotion;
        angularZMotion = ConfigurableJoint.angularZMotion;
        linearLimitSpring = ConfigurableJoint.linearLimitSpring;
        angularXLimitSpring = ConfigurableJoint.angularXLimitSpring;
        angularYZLimitSpring = ConfigurableJoint.angularYZLimitSpring;
        linearLimit = ConfigurableJoint.linearLimit;
        lowAngularXLimit = ConfigurableJoint.lowAngularXLimit;
        highAngularXLimit = ConfigurableJoint.highAngularXLimit;
        angularYLimit = ConfigurableJoint.angularYLimit;
        angularZLimit = ConfigurableJoint.angularZLimit;
        targetPosition = ConfigurableJoint.targetPosition;
        targetVelocity = ConfigurableJoint.targetVelocity;
        xDrive = ConfigurableJoint.xDrive;
        yDrive = ConfigurableJoint.yDrive;
        zDrive = ConfigurableJoint.zDrive;
        targetRotation = ConfigurableJoint.targetRotation;
        targetAngularVelocity = ConfigurableJoint.targetAngularVelocity;
        rotationDriveMode = ConfigurableJoint.rotationDriveMode;
        angularXDrive = ConfigurableJoint.angularXDrive;
        angularYZDrive = ConfigurableJoint.angularYZDrive;
        slerpDrive = ConfigurableJoint.slerpDrive;
        projectionMode = ConfigurableJoint.projectionMode;
        projectionDistance = ConfigurableJoint.projectionDistance;
        projectionAngle = ConfigurableJoint.projectionAngle;
        configuredInWorldSpace = ConfigurableJoint.configuredInWorldSpace;
        swapBodies = ConfigurableJoint.swapBodies;
        connectedBody = ConfigurableJoint.connectedBody;
        connectedArticulationBody = ConfigurableJoint.connectedArticulationBody;
        axis = ConfigurableJoint.axis;
        anchor = ConfigurableJoint.anchor;
        connectedAnchor = ConfigurableJoint.connectedAnchor;
        autoConfigureConnectedAnchor = ConfigurableJoint.autoConfigureConnectedAnchor;
        breakForce = ConfigurableJoint.breakForce;
        breakTorque = ConfigurableJoint.breakTorque;
        enableCollision = ConfigurableJoint.enableCollision;
        enablePreprocessing = ConfigurableJoint.enablePreprocessing;
        massScale = ConfigurableJoint.massScale;
        connectedMassScale = ConfigurableJoint.connectedMassScale;
        tag = ConfigurableJoint.tag;
        name = ConfigurableJoint.name;
        hideFlags = ConfigurableJoint.hideFlags;
    }
}
[System.Serializable]
public class ArticulationBodyZSaver : ZSave.ZSaver<UnityEngine.ArticulationBody> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ArticulationBodyZSaver (UnityEngine.ArticulationBody ArticulationBody) : base(ArticulationBody.gameObject, ArticulationBody) {
        jointType = ArticulationBody.jointType;
        anchorPosition = ArticulationBody.anchorPosition;
        parentAnchorPosition = ArticulationBody.parentAnchorPosition;
        anchorRotation = ArticulationBody.anchorRotation;
        parentAnchorRotation = ArticulationBody.parentAnchorRotation;
        linearLockX = ArticulationBody.linearLockX;
        linearLockY = ArticulationBody.linearLockY;
        linearLockZ = ArticulationBody.linearLockZ;
        swingYLock = ArticulationBody.swingYLock;
        swingZLock = ArticulationBody.swingZLock;
        twistLock = ArticulationBody.twistLock;
        xDrive = ArticulationBody.xDrive;
        yDrive = ArticulationBody.yDrive;
        zDrive = ArticulationBody.zDrive;
        immovable = ArticulationBody.immovable;
        useGravity = ArticulationBody.useGravity;
        linearDamping = ArticulationBody.linearDamping;
        angularDamping = ArticulationBody.angularDamping;
        jointFriction = ArticulationBody.jointFriction;
        velocity = ArticulationBody.velocity;
        angularVelocity = ArticulationBody.angularVelocity;
        mass = ArticulationBody.mass;
        centerOfMass = ArticulationBody.centerOfMass;
        inertiaTensor = ArticulationBody.inertiaTensor;
        inertiaTensorRotation = ArticulationBody.inertiaTensorRotation;
        sleepThreshold = ArticulationBody.sleepThreshold;
        solverIterations = ArticulationBody.solverIterations;
        solverVelocityIterations = ArticulationBody.solverVelocityIterations;
        maxAngularVelocity = ArticulationBody.maxAngularVelocity;
        maxLinearVelocity = ArticulationBody.maxLinearVelocity;
        maxJointVelocity = ArticulationBody.maxJointVelocity;
        maxDepenetrationVelocity = ArticulationBody.maxDepenetrationVelocity;
        jointPosition = ArticulationBody.jointPosition;
        jointVelocity = ArticulationBody.jointVelocity;
        jointAcceleration = ArticulationBody.jointAcceleration;
        jointForce = ArticulationBody.jointForce;
        enabled = ArticulationBody.enabled;
        tag = ArticulationBody.tag;
        name = ArticulationBody.name;
        hideFlags = ArticulationBody.hideFlags;
    }
}
[System.Serializable]
public class Rigidbody2DZSaver : ZSave.ZSaver<UnityEngine.Rigidbody2D> {
    public UnityEngine.Vector2 position;
    public System.Single rotation;
    public UnityEngine.Vector2 velocity;
    public System.Single angularVelocity;
    public System.Boolean useAutoMass;
    public System.Single mass;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public Rigidbody2DZSaver (UnityEngine.Rigidbody2D Rigidbody2D) : base(Rigidbody2D.gameObject, Rigidbody2D) {
        position = Rigidbody2D.position;
        rotation = Rigidbody2D.rotation;
        velocity = Rigidbody2D.velocity;
        angularVelocity = Rigidbody2D.angularVelocity;
        useAutoMass = Rigidbody2D.useAutoMass;
        mass = Rigidbody2D.mass;
        sharedMaterial = Rigidbody2D.sharedMaterial;
        centerOfMass = Rigidbody2D.centerOfMass;
        inertia = Rigidbody2D.inertia;
        drag = Rigidbody2D.drag;
        angularDrag = Rigidbody2D.angularDrag;
        gravityScale = Rigidbody2D.gravityScale;
        bodyType = Rigidbody2D.bodyType;
        useFullKinematicContacts = Rigidbody2D.useFullKinematicContacts;
        isKinematic = Rigidbody2D.isKinematic;
        freezeRotation = Rigidbody2D.freezeRotation;
        constraints = Rigidbody2D.constraints;
        simulated = Rigidbody2D.simulated;
        interpolation = Rigidbody2D.interpolation;
        sleepMode = Rigidbody2D.sleepMode;
        collisionDetectionMode = Rigidbody2D.collisionDetectionMode;
        tag = Rigidbody2D.tag;
        name = Rigidbody2D.name;
        hideFlags = Rigidbody2D.hideFlags;
    }
}
[System.Serializable]
public class Collider2DZSaver : ZSave.ZSaver<UnityEngine.Collider2D> {
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public Collider2DZSaver (UnityEngine.Collider2D Collider2D) : base(Collider2D.gameObject, Collider2D) {
        density = Collider2D.density;
        isTrigger = Collider2D.isTrigger;
        usedByEffector = Collider2D.usedByEffector;
        usedByComposite = Collider2D.usedByComposite;
        offset = Collider2D.offset;
        sharedMaterial = Collider2D.sharedMaterial;
        enabled = Collider2D.enabled;
        tag = Collider2D.tag;
        name = Collider2D.name;
        hideFlags = Collider2D.hideFlags;
    }
}
[System.Serializable]
public class CircleCollider2DZSaver : ZSave.ZSaver<UnityEngine.CircleCollider2D> {
    public System.Single radius;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CircleCollider2DZSaver (UnityEngine.CircleCollider2D CircleCollider2D) : base(CircleCollider2D.gameObject, CircleCollider2D) {
        radius = CircleCollider2D.radius;
        density = CircleCollider2D.density;
        isTrigger = CircleCollider2D.isTrigger;
        usedByEffector = CircleCollider2D.usedByEffector;
        usedByComposite = CircleCollider2D.usedByComposite;
        offset = CircleCollider2D.offset;
        sharedMaterial = CircleCollider2D.sharedMaterial;
        enabled = CircleCollider2D.enabled;
        tag = CircleCollider2D.tag;
        name = CircleCollider2D.name;
        hideFlags = CircleCollider2D.hideFlags;
    }
}
[System.Serializable]
public class CapsuleCollider2DZSaver : ZSave.ZSaver<UnityEngine.CapsuleCollider2D> {
    public UnityEngine.Vector2 size;
    public UnityEngine.CapsuleDirection2D direction;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CapsuleCollider2DZSaver (UnityEngine.CapsuleCollider2D CapsuleCollider2D) : base(CapsuleCollider2D.gameObject, CapsuleCollider2D) {
        size = CapsuleCollider2D.size;
        direction = CapsuleCollider2D.direction;
        density = CapsuleCollider2D.density;
        isTrigger = CapsuleCollider2D.isTrigger;
        usedByEffector = CapsuleCollider2D.usedByEffector;
        usedByComposite = CapsuleCollider2D.usedByComposite;
        offset = CapsuleCollider2D.offset;
        sharedMaterial = CapsuleCollider2D.sharedMaterial;
        enabled = CapsuleCollider2D.enabled;
        tag = CapsuleCollider2D.tag;
        name = CapsuleCollider2D.name;
        hideFlags = CapsuleCollider2D.hideFlags;
    }
}
[System.Serializable]
public class EdgeCollider2DZSaver : ZSave.ZSaver<UnityEngine.EdgeCollider2D> {
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
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public EdgeCollider2DZSaver (UnityEngine.EdgeCollider2D EdgeCollider2D) : base(EdgeCollider2D.gameObject, EdgeCollider2D) {
        edgeRadius = EdgeCollider2D.edgeRadius;
        points = EdgeCollider2D.points;
        useAdjacentStartPoint = EdgeCollider2D.useAdjacentStartPoint;
        useAdjacentEndPoint = EdgeCollider2D.useAdjacentEndPoint;
        adjacentStartPoint = EdgeCollider2D.adjacentStartPoint;
        adjacentEndPoint = EdgeCollider2D.adjacentEndPoint;
        density = EdgeCollider2D.density;
        isTrigger = EdgeCollider2D.isTrigger;
        usedByEffector = EdgeCollider2D.usedByEffector;
        usedByComposite = EdgeCollider2D.usedByComposite;
        offset = EdgeCollider2D.offset;
        sharedMaterial = EdgeCollider2D.sharedMaterial;
        enabled = EdgeCollider2D.enabled;
        tag = EdgeCollider2D.tag;
        name = EdgeCollider2D.name;
        hideFlags = EdgeCollider2D.hideFlags;
    }
}
[System.Serializable]
public class BoxCollider2DZSaver : ZSave.ZSaver<UnityEngine.BoxCollider2D> {
    public UnityEngine.Vector2 size;
    public System.Single edgeRadius;
    public System.Boolean autoTiling;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public BoxCollider2DZSaver (UnityEngine.BoxCollider2D BoxCollider2D) : base(BoxCollider2D.gameObject, BoxCollider2D) {
        size = BoxCollider2D.size;
        edgeRadius = BoxCollider2D.edgeRadius;
        autoTiling = BoxCollider2D.autoTiling;
        density = BoxCollider2D.density;
        isTrigger = BoxCollider2D.isTrigger;
        usedByEffector = BoxCollider2D.usedByEffector;
        usedByComposite = BoxCollider2D.usedByComposite;
        offset = BoxCollider2D.offset;
        sharedMaterial = BoxCollider2D.sharedMaterial;
        enabled = BoxCollider2D.enabled;
        tag = BoxCollider2D.tag;
        name = BoxCollider2D.name;
        hideFlags = BoxCollider2D.hideFlags;
    }
}
[System.Serializable]
public class PolygonCollider2DZSaver : ZSave.ZSaver<UnityEngine.PolygonCollider2D> {
    public System.Boolean autoTiling;
    public UnityEngine.Vector2[] points;
    public System.Int32 pathCount;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PolygonCollider2DZSaver (UnityEngine.PolygonCollider2D PolygonCollider2D) : base(PolygonCollider2D.gameObject, PolygonCollider2D) {
        autoTiling = PolygonCollider2D.autoTiling;
        points = PolygonCollider2D.points;
        pathCount = PolygonCollider2D.pathCount;
        density = PolygonCollider2D.density;
        isTrigger = PolygonCollider2D.isTrigger;
        usedByEffector = PolygonCollider2D.usedByEffector;
        usedByComposite = PolygonCollider2D.usedByComposite;
        offset = PolygonCollider2D.offset;
        sharedMaterial = PolygonCollider2D.sharedMaterial;
        enabled = PolygonCollider2D.enabled;
        tag = PolygonCollider2D.tag;
        name = PolygonCollider2D.name;
        hideFlags = PolygonCollider2D.hideFlags;
    }
}
[System.Serializable]
public class CompositeCollider2DZSaver : ZSave.ZSaver<UnityEngine.CompositeCollider2D> {
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
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CompositeCollider2DZSaver (UnityEngine.CompositeCollider2D CompositeCollider2D) : base(CompositeCollider2D.gameObject, CompositeCollider2D) {
        geometryType = CompositeCollider2D.geometryType;
        generationType = CompositeCollider2D.generationType;
        vertexDistance = CompositeCollider2D.vertexDistance;
        edgeRadius = CompositeCollider2D.edgeRadius;
        offsetDistance = CompositeCollider2D.offsetDistance;
        density = CompositeCollider2D.density;
        isTrigger = CompositeCollider2D.isTrigger;
        usedByEffector = CompositeCollider2D.usedByEffector;
        usedByComposite = CompositeCollider2D.usedByComposite;
        offset = CompositeCollider2D.offset;
        sharedMaterial = CompositeCollider2D.sharedMaterial;
        enabled = CompositeCollider2D.enabled;
        tag = CompositeCollider2D.tag;
        name = CompositeCollider2D.name;
        hideFlags = CompositeCollider2D.hideFlags;
    }
}
[System.Serializable]
public class Joint2DZSaver : ZSave.ZSaver<UnityEngine.Joint2D> {
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public Joint2DZSaver (UnityEngine.Joint2D Joint2D) : base(Joint2D.gameObject, Joint2D) {
        connectedBody = Joint2D.connectedBody;
        enableCollision = Joint2D.enableCollision;
        breakForce = Joint2D.breakForce;
        breakTorque = Joint2D.breakTorque;
        enabled = Joint2D.enabled;
        tag = Joint2D.tag;
        name = Joint2D.name;
        hideFlags = Joint2D.hideFlags;
    }
}
[System.Serializable]
public class AnchoredJoint2DZSaver : ZSave.ZSaver<UnityEngine.AnchoredJoint2D> {
    public UnityEngine.Vector2 anchor;
    public UnityEngine.Vector2 connectedAnchor;
    public System.Boolean autoConfigureConnectedAnchor;
    public UnityEngine.Rigidbody2D connectedBody;
    public System.Boolean enableCollision;
    public System.Single breakForce;
    public System.Single breakTorque;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AnchoredJoint2DZSaver (UnityEngine.AnchoredJoint2D AnchoredJoint2D) : base(AnchoredJoint2D.gameObject, AnchoredJoint2D) {
        anchor = AnchoredJoint2D.anchor;
        connectedAnchor = AnchoredJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = AnchoredJoint2D.autoConfigureConnectedAnchor;
        connectedBody = AnchoredJoint2D.connectedBody;
        enableCollision = AnchoredJoint2D.enableCollision;
        breakForce = AnchoredJoint2D.breakForce;
        breakTorque = AnchoredJoint2D.breakTorque;
        enabled = AnchoredJoint2D.enabled;
        tag = AnchoredJoint2D.tag;
        name = AnchoredJoint2D.name;
        hideFlags = AnchoredJoint2D.hideFlags;
    }
}
[System.Serializable]
public class SpringJoint2DZSaver : ZSave.ZSaver<UnityEngine.SpringJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SpringJoint2DZSaver (UnityEngine.SpringJoint2D SpringJoint2D) : base(SpringJoint2D.gameObject, SpringJoint2D) {
        autoConfigureDistance = SpringJoint2D.autoConfigureDistance;
        distance = SpringJoint2D.distance;
        dampingRatio = SpringJoint2D.dampingRatio;
        frequency = SpringJoint2D.frequency;
        anchor = SpringJoint2D.anchor;
        connectedAnchor = SpringJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = SpringJoint2D.autoConfigureConnectedAnchor;
        connectedBody = SpringJoint2D.connectedBody;
        enableCollision = SpringJoint2D.enableCollision;
        breakForce = SpringJoint2D.breakForce;
        breakTorque = SpringJoint2D.breakTorque;
        enabled = SpringJoint2D.enabled;
        tag = SpringJoint2D.tag;
        name = SpringJoint2D.name;
        hideFlags = SpringJoint2D.hideFlags;
    }
}
[System.Serializable]
public class DistanceJoint2DZSaver : ZSave.ZSaver<UnityEngine.DistanceJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public DistanceJoint2DZSaver (UnityEngine.DistanceJoint2D DistanceJoint2D) : base(DistanceJoint2D.gameObject, DistanceJoint2D) {
        autoConfigureDistance = DistanceJoint2D.autoConfigureDistance;
        distance = DistanceJoint2D.distance;
        maxDistanceOnly = DistanceJoint2D.maxDistanceOnly;
        anchor = DistanceJoint2D.anchor;
        connectedAnchor = DistanceJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = DistanceJoint2D.autoConfigureConnectedAnchor;
        connectedBody = DistanceJoint2D.connectedBody;
        enableCollision = DistanceJoint2D.enableCollision;
        breakForce = DistanceJoint2D.breakForce;
        breakTorque = DistanceJoint2D.breakTorque;
        enabled = DistanceJoint2D.enabled;
        tag = DistanceJoint2D.tag;
        name = DistanceJoint2D.name;
        hideFlags = DistanceJoint2D.hideFlags;
    }
}
[System.Serializable]
public class FrictionJoint2DZSaver : ZSave.ZSaver<UnityEngine.FrictionJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public FrictionJoint2DZSaver (UnityEngine.FrictionJoint2D FrictionJoint2D) : base(FrictionJoint2D.gameObject, FrictionJoint2D) {
        maxForce = FrictionJoint2D.maxForce;
        maxTorque = FrictionJoint2D.maxTorque;
        anchor = FrictionJoint2D.anchor;
        connectedAnchor = FrictionJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = FrictionJoint2D.autoConfigureConnectedAnchor;
        connectedBody = FrictionJoint2D.connectedBody;
        enableCollision = FrictionJoint2D.enableCollision;
        breakForce = FrictionJoint2D.breakForce;
        breakTorque = FrictionJoint2D.breakTorque;
        enabled = FrictionJoint2D.enabled;
        tag = FrictionJoint2D.tag;
        name = FrictionJoint2D.name;
        hideFlags = FrictionJoint2D.hideFlags;
    }
}
[System.Serializable]
public class HingeJoint2DZSaver : ZSave.ZSaver<UnityEngine.HingeJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public HingeJoint2DZSaver (UnityEngine.HingeJoint2D HingeJoint2D) : base(HingeJoint2D.gameObject, HingeJoint2D) {
        useMotor = HingeJoint2D.useMotor;
        useLimits = HingeJoint2D.useLimits;
        motor = HingeJoint2D.motor;
        limits = HingeJoint2D.limits;
        anchor = HingeJoint2D.anchor;
        connectedAnchor = HingeJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = HingeJoint2D.autoConfigureConnectedAnchor;
        connectedBody = HingeJoint2D.connectedBody;
        enableCollision = HingeJoint2D.enableCollision;
        breakForce = HingeJoint2D.breakForce;
        breakTorque = HingeJoint2D.breakTorque;
        enabled = HingeJoint2D.enabled;
        tag = HingeJoint2D.tag;
        name = HingeJoint2D.name;
        hideFlags = HingeJoint2D.hideFlags;
    }
}
[System.Serializable]
public class RelativeJoint2DZSaver : ZSave.ZSaver<UnityEngine.RelativeJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public RelativeJoint2DZSaver (UnityEngine.RelativeJoint2D RelativeJoint2D) : base(RelativeJoint2D.gameObject, RelativeJoint2D) {
        maxForce = RelativeJoint2D.maxForce;
        maxTorque = RelativeJoint2D.maxTorque;
        correctionScale = RelativeJoint2D.correctionScale;
        autoConfigureOffset = RelativeJoint2D.autoConfigureOffset;
        linearOffset = RelativeJoint2D.linearOffset;
        angularOffset = RelativeJoint2D.angularOffset;
        connectedBody = RelativeJoint2D.connectedBody;
        enableCollision = RelativeJoint2D.enableCollision;
        breakForce = RelativeJoint2D.breakForce;
        breakTorque = RelativeJoint2D.breakTorque;
        enabled = RelativeJoint2D.enabled;
        tag = RelativeJoint2D.tag;
        name = RelativeJoint2D.name;
        hideFlags = RelativeJoint2D.hideFlags;
    }
}
[System.Serializable]
public class SliderJoint2DZSaver : ZSave.ZSaver<UnityEngine.SliderJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SliderJoint2DZSaver (UnityEngine.SliderJoint2D SliderJoint2D) : base(SliderJoint2D.gameObject, SliderJoint2D) {
        autoConfigureAngle = SliderJoint2D.autoConfigureAngle;
        angle = SliderJoint2D.angle;
        useMotor = SliderJoint2D.useMotor;
        useLimits = SliderJoint2D.useLimits;
        motor = SliderJoint2D.motor;
        limits = SliderJoint2D.limits;
        anchor = SliderJoint2D.anchor;
        connectedAnchor = SliderJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = SliderJoint2D.autoConfigureConnectedAnchor;
        connectedBody = SliderJoint2D.connectedBody;
        enableCollision = SliderJoint2D.enableCollision;
        breakForce = SliderJoint2D.breakForce;
        breakTorque = SliderJoint2D.breakTorque;
        enabled = SliderJoint2D.enabled;
        tag = SliderJoint2D.tag;
        name = SliderJoint2D.name;
        hideFlags = SliderJoint2D.hideFlags;
    }
}
[System.Serializable]
public class TargetJoint2DZSaver : ZSave.ZSaver<UnityEngine.TargetJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TargetJoint2DZSaver (UnityEngine.TargetJoint2D TargetJoint2D) : base(TargetJoint2D.gameObject, TargetJoint2D) {
        anchor = TargetJoint2D.anchor;
        target = TargetJoint2D.target;
        autoConfigureTarget = TargetJoint2D.autoConfigureTarget;
        maxForce = TargetJoint2D.maxForce;
        dampingRatio = TargetJoint2D.dampingRatio;
        frequency = TargetJoint2D.frequency;
        connectedBody = TargetJoint2D.connectedBody;
        enableCollision = TargetJoint2D.enableCollision;
        breakForce = TargetJoint2D.breakForce;
        breakTorque = TargetJoint2D.breakTorque;
        enabled = TargetJoint2D.enabled;
        tag = TargetJoint2D.tag;
        name = TargetJoint2D.name;
        hideFlags = TargetJoint2D.hideFlags;
    }
}
[System.Serializable]
public class FixedJoint2DZSaver : ZSave.ZSaver<UnityEngine.FixedJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public FixedJoint2DZSaver (UnityEngine.FixedJoint2D FixedJoint2D) : base(FixedJoint2D.gameObject, FixedJoint2D) {
        dampingRatio = FixedJoint2D.dampingRatio;
        frequency = FixedJoint2D.frequency;
        anchor = FixedJoint2D.anchor;
        connectedAnchor = FixedJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = FixedJoint2D.autoConfigureConnectedAnchor;
        connectedBody = FixedJoint2D.connectedBody;
        enableCollision = FixedJoint2D.enableCollision;
        breakForce = FixedJoint2D.breakForce;
        breakTorque = FixedJoint2D.breakTorque;
        enabled = FixedJoint2D.enabled;
        tag = FixedJoint2D.tag;
        name = FixedJoint2D.name;
        hideFlags = FixedJoint2D.hideFlags;
    }
}
[System.Serializable]
public class WheelJoint2DZSaver : ZSave.ZSaver<UnityEngine.WheelJoint2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public WheelJoint2DZSaver (UnityEngine.WheelJoint2D WheelJoint2D) : base(WheelJoint2D.gameObject, WheelJoint2D) {
        suspension = WheelJoint2D.suspension;
        useMotor = WheelJoint2D.useMotor;
        motor = WheelJoint2D.motor;
        anchor = WheelJoint2D.anchor;
        connectedAnchor = WheelJoint2D.connectedAnchor;
        autoConfigureConnectedAnchor = WheelJoint2D.autoConfigureConnectedAnchor;
        connectedBody = WheelJoint2D.connectedBody;
        enableCollision = WheelJoint2D.enableCollision;
        breakForce = WheelJoint2D.breakForce;
        breakTorque = WheelJoint2D.breakTorque;
        enabled = WheelJoint2D.enabled;
        tag = WheelJoint2D.tag;
        name = WheelJoint2D.name;
        hideFlags = WheelJoint2D.hideFlags;
    }
}
[System.Serializable]
public class Effector2DZSaver : ZSave.ZSaver<UnityEngine.Effector2D> {
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public Effector2DZSaver (UnityEngine.Effector2D Effector2D) : base(Effector2D.gameObject, Effector2D) {
        useColliderMask = Effector2D.useColliderMask;
        colliderMask = Effector2D.colliderMask;
        enabled = Effector2D.enabled;
        tag = Effector2D.tag;
        name = Effector2D.name;
        hideFlags = Effector2D.hideFlags;
    }
}
[System.Serializable]
public class AreaEffector2DZSaver : ZSave.ZSaver<UnityEngine.AreaEffector2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public AreaEffector2DZSaver (UnityEngine.AreaEffector2D AreaEffector2D) : base(AreaEffector2D.gameObject, AreaEffector2D) {
        forceAngle = AreaEffector2D.forceAngle;
        useGlobalAngle = AreaEffector2D.useGlobalAngle;
        forceMagnitude = AreaEffector2D.forceMagnitude;
        forceVariation = AreaEffector2D.forceVariation;
        drag = AreaEffector2D.drag;
        angularDrag = AreaEffector2D.angularDrag;
        forceTarget = AreaEffector2D.forceTarget;
        useColliderMask = AreaEffector2D.useColliderMask;
        colliderMask = AreaEffector2D.colliderMask;
        enabled = AreaEffector2D.enabled;
        tag = AreaEffector2D.tag;
        name = AreaEffector2D.name;
        hideFlags = AreaEffector2D.hideFlags;
    }
}
[System.Serializable]
public class BuoyancyEffector2DZSaver : ZSave.ZSaver<UnityEngine.BuoyancyEffector2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public BuoyancyEffector2DZSaver (UnityEngine.BuoyancyEffector2D BuoyancyEffector2D) : base(BuoyancyEffector2D.gameObject, BuoyancyEffector2D) {
        surfaceLevel = BuoyancyEffector2D.surfaceLevel;
        density = BuoyancyEffector2D.density;
        linearDrag = BuoyancyEffector2D.linearDrag;
        angularDrag = BuoyancyEffector2D.angularDrag;
        flowAngle = BuoyancyEffector2D.flowAngle;
        flowMagnitude = BuoyancyEffector2D.flowMagnitude;
        flowVariation = BuoyancyEffector2D.flowVariation;
        useColliderMask = BuoyancyEffector2D.useColliderMask;
        colliderMask = BuoyancyEffector2D.colliderMask;
        enabled = BuoyancyEffector2D.enabled;
        tag = BuoyancyEffector2D.tag;
        name = BuoyancyEffector2D.name;
        hideFlags = BuoyancyEffector2D.hideFlags;
    }
}
[System.Serializable]
public class PointEffector2DZSaver : ZSave.ZSaver<UnityEngine.PointEffector2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PointEffector2DZSaver (UnityEngine.PointEffector2D PointEffector2D) : base(PointEffector2D.gameObject, PointEffector2D) {
        forceMagnitude = PointEffector2D.forceMagnitude;
        forceVariation = PointEffector2D.forceVariation;
        distanceScale = PointEffector2D.distanceScale;
        drag = PointEffector2D.drag;
        angularDrag = PointEffector2D.angularDrag;
        forceSource = PointEffector2D.forceSource;
        forceTarget = PointEffector2D.forceTarget;
        forceMode = PointEffector2D.forceMode;
        useColliderMask = PointEffector2D.useColliderMask;
        colliderMask = PointEffector2D.colliderMask;
        enabled = PointEffector2D.enabled;
        tag = PointEffector2D.tag;
        name = PointEffector2D.name;
        hideFlags = PointEffector2D.hideFlags;
    }
}
[System.Serializable]
public class PlatformEffector2DZSaver : ZSave.ZSaver<UnityEngine.PlatformEffector2D> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PlatformEffector2DZSaver (UnityEngine.PlatformEffector2D PlatformEffector2D) : base(PlatformEffector2D.gameObject, PlatformEffector2D) {
        useOneWay = PlatformEffector2D.useOneWay;
        useOneWayGrouping = PlatformEffector2D.useOneWayGrouping;
        useSideFriction = PlatformEffector2D.useSideFriction;
        useSideBounce = PlatformEffector2D.useSideBounce;
        surfaceArc = PlatformEffector2D.surfaceArc;
        sideArc = PlatformEffector2D.sideArc;
        rotationalOffset = PlatformEffector2D.rotationalOffset;
        useColliderMask = PlatformEffector2D.useColliderMask;
        colliderMask = PlatformEffector2D.colliderMask;
        enabled = PlatformEffector2D.enabled;
        tag = PlatformEffector2D.tag;
        name = PlatformEffector2D.name;
        hideFlags = PlatformEffector2D.hideFlags;
    }
}
[System.Serializable]
public class SurfaceEffector2DZSaver : ZSave.ZSaver<UnityEngine.SurfaceEffector2D> {
    public System.Single speed;
    public System.Single speedVariation;
    public System.Single forceScale;
    public System.Boolean useContactForce;
    public System.Boolean useFriction;
    public System.Boolean useBounce;
    public System.Boolean useColliderMask;
    public System.Int32 colliderMask;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SurfaceEffector2DZSaver (UnityEngine.SurfaceEffector2D SurfaceEffector2D) : base(SurfaceEffector2D.gameObject, SurfaceEffector2D) {
        speed = SurfaceEffector2D.speed;
        speedVariation = SurfaceEffector2D.speedVariation;
        forceScale = SurfaceEffector2D.forceScale;
        useContactForce = SurfaceEffector2D.useContactForce;
        useFriction = SurfaceEffector2D.useFriction;
        useBounce = SurfaceEffector2D.useBounce;
        useColliderMask = SurfaceEffector2D.useColliderMask;
        colliderMask = SurfaceEffector2D.colliderMask;
        enabled = SurfaceEffector2D.enabled;
        tag = SurfaceEffector2D.tag;
        name = SurfaceEffector2D.name;
        hideFlags = SurfaceEffector2D.hideFlags;
    }
}
[System.Serializable]
public class PhysicsUpdateBehaviour2DZSaver : ZSave.ZSaver<UnityEngine.PhysicsUpdateBehaviour2D> {
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PhysicsUpdateBehaviour2DZSaver (UnityEngine.PhysicsUpdateBehaviour2D PhysicsUpdateBehaviour2D) : base(PhysicsUpdateBehaviour2D.gameObject, PhysicsUpdateBehaviour2D) {
        enabled = PhysicsUpdateBehaviour2D.enabled;
        tag = PhysicsUpdateBehaviour2D.tag;
        name = PhysicsUpdateBehaviour2D.name;
        hideFlags = PhysicsUpdateBehaviour2D.hideFlags;
    }
}
[System.Serializable]
public class ConstantForce2DZSaver : ZSave.ZSaver<UnityEngine.ConstantForce2D> {
    public UnityEngine.Vector2 force;
    public UnityEngine.Vector2 relativeForce;
    public System.Single torque;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public ConstantForce2DZSaver (UnityEngine.ConstantForce2D ConstantForce2D) : base(ConstantForce2D.gameObject, ConstantForce2D) {
        force = ConstantForce2D.force;
        relativeForce = ConstantForce2D.relativeForce;
        torque = ConstantForce2D.torque;
        enabled = ConstantForce2D.enabled;
        tag = ConstantForce2D.tag;
        name = ConstantForce2D.name;
        hideFlags = ConstantForce2D.hideFlags;
    }
}
[System.Serializable]
public class SpriteMaskZSaver : ZSave.ZSaver<UnityEngine.SpriteMask> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SpriteMaskZSaver (UnityEngine.SpriteMask SpriteMask) : base(SpriteMask.gameObject, SpriteMask) {
        frontSortingLayerID = SpriteMask.frontSortingLayerID;
        frontSortingOrder = SpriteMask.frontSortingOrder;
        backSortingLayerID = SpriteMask.backSortingLayerID;
        backSortingOrder = SpriteMask.backSortingOrder;
        alphaCutoff = SpriteMask.alphaCutoff;
        sprite = SpriteMask.sprite;
        isCustomRangeActive = SpriteMask.isCustomRangeActive;
        spriteSortPoint = SpriteMask.spriteSortPoint;
        enabled = SpriteMask.enabled;
        shadowCastingMode = SpriteMask.shadowCastingMode;
        receiveShadows = SpriteMask.receiveShadows;
        forceRenderingOff = SpriteMask.forceRenderingOff;
        motionVectorGenerationMode = SpriteMask.motionVectorGenerationMode;
        lightProbeUsage = SpriteMask.lightProbeUsage;
        reflectionProbeUsage = SpriteMask.reflectionProbeUsage;
        renderingLayerMask = SpriteMask.renderingLayerMask;
        rendererPriority = SpriteMask.rendererPriority;
        rayTracingMode = SpriteMask.rayTracingMode;
        sortingLayerName = SpriteMask.sortingLayerName;
        sortingLayerID = SpriteMask.sortingLayerID;
        sortingOrder = SpriteMask.sortingOrder;
        allowOcclusionWhenDynamic = SpriteMask.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SpriteMask.lightProbeProxyVolumeOverride;
        probeAnchor = SpriteMask.probeAnchor;
        lightmapIndex = SpriteMask.lightmapIndex;
        realtimeLightmapIndex = SpriteMask.realtimeLightmapIndex;
        lightmapScaleOffset = SpriteMask.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SpriteMask.realtimeLightmapScaleOffset;
        materials = SpriteMask.materials;
        material = SpriteMask.material;
        sharedMaterial = SpriteMask.sharedMaterial;
        sharedMaterials = SpriteMask.sharedMaterials;
        tag = SpriteMask.tag;
        name = SpriteMask.name;
        hideFlags = SpriteMask.hideFlags;
    }
}
[System.Serializable]
public class SpriteShapeRendererZSaver : ZSave.ZSaver<UnityEngine.U2D.SpriteShapeRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public SpriteShapeRendererZSaver (UnityEngine.U2D.SpriteShapeRenderer SpriteShapeRenderer) : base(SpriteShapeRenderer.gameObject, SpriteShapeRenderer) {
        color = SpriteShapeRenderer.color;
        maskInteraction = SpriteShapeRenderer.maskInteraction;
        enabled = SpriteShapeRenderer.enabled;
        shadowCastingMode = SpriteShapeRenderer.shadowCastingMode;
        receiveShadows = SpriteShapeRenderer.receiveShadows;
        forceRenderingOff = SpriteShapeRenderer.forceRenderingOff;
        motionVectorGenerationMode = SpriteShapeRenderer.motionVectorGenerationMode;
        lightProbeUsage = SpriteShapeRenderer.lightProbeUsage;
        reflectionProbeUsage = SpriteShapeRenderer.reflectionProbeUsage;
        renderingLayerMask = SpriteShapeRenderer.renderingLayerMask;
        rendererPriority = SpriteShapeRenderer.rendererPriority;
        rayTracingMode = SpriteShapeRenderer.rayTracingMode;
        sortingLayerName = SpriteShapeRenderer.sortingLayerName;
        sortingLayerID = SpriteShapeRenderer.sortingLayerID;
        sortingOrder = SpriteShapeRenderer.sortingOrder;
        allowOcclusionWhenDynamic = SpriteShapeRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = SpriteShapeRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = SpriteShapeRenderer.probeAnchor;
        lightmapIndex = SpriteShapeRenderer.lightmapIndex;
        realtimeLightmapIndex = SpriteShapeRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = SpriteShapeRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = SpriteShapeRenderer.realtimeLightmapScaleOffset;
        materials = SpriteShapeRenderer.materials;
        material = SpriteShapeRenderer.material;
        sharedMaterial = SpriteShapeRenderer.sharedMaterial;
        sharedMaterials = SpriteShapeRenderer.sharedMaterials;
        tag = SpriteShapeRenderer.tag;
        name = SpriteShapeRenderer.name;
        hideFlags = SpriteShapeRenderer.hideFlags;
    }
}
[System.Serializable]
public class StreamingControllerZSaver : ZSave.ZSaver<UnityEngine.StreamingController> {
    public System.Single streamingMipmapBias;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public StreamingControllerZSaver (UnityEngine.StreamingController StreamingController) : base(StreamingController.gameObject, StreamingController) {
        streamingMipmapBias = StreamingController.streamingMipmapBias;
        enabled = StreamingController.enabled;
        tag = StreamingController.tag;
        name = StreamingController.name;
        hideFlags = StreamingController.hideFlags;
    }
}
[System.Serializable]
public class TerrainZSaver : ZSave.ZSaver<UnityEngine.Terrain> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TerrainZSaver (UnityEngine.Terrain Terrain) : base(Terrain.gameObject, Terrain) {
        terrainData = Terrain.terrainData;
        treeDistance = Terrain.treeDistance;
        treeBillboardDistance = Terrain.treeBillboardDistance;
        treeCrossFadeLength = Terrain.treeCrossFadeLength;
        treeMaximumFullLODCount = Terrain.treeMaximumFullLODCount;
        detailObjectDistance = Terrain.detailObjectDistance;
        detailObjectDensity = Terrain.detailObjectDensity;
        heightmapPixelError = Terrain.heightmapPixelError;
        heightmapMaximumLOD = Terrain.heightmapMaximumLOD;
        basemapDistance = Terrain.basemapDistance;
        lightmapIndex = Terrain.lightmapIndex;
        realtimeLightmapIndex = Terrain.realtimeLightmapIndex;
        lightmapScaleOffset = Terrain.lightmapScaleOffset;
        realtimeLightmapScaleOffset = Terrain.realtimeLightmapScaleOffset;
        freeUnusedRenderingResources = Terrain.freeUnusedRenderingResources;
        shadowCastingMode = Terrain.shadowCastingMode;
        reflectionProbeUsage = Terrain.reflectionProbeUsage;
        materialTemplate = Terrain.materialTemplate;
        drawHeightmap = Terrain.drawHeightmap;
        allowAutoConnect = Terrain.allowAutoConnect;
        groupingID = Terrain.groupingID;
        drawInstanced = Terrain.drawInstanced;
        drawTreesAndFoliage = Terrain.drawTreesAndFoliage;
        patchBoundsMultiplier = Terrain.patchBoundsMultiplier;
        treeLODBiasMultiplier = Terrain.treeLODBiasMultiplier;
        collectDetailPatches = Terrain.collectDetailPatches;
        editorRenderFlags = Terrain.editorRenderFlags;
        preserveTreePrototypeLayers = Terrain.preserveTreePrototypeLayers;
        renderingLayerMask = Terrain.renderingLayerMask;
        enabled = Terrain.enabled;
        tag = Terrain.tag;
        name = Terrain.name;
        hideFlags = Terrain.hideFlags;
    }
}
[System.Serializable]
public class TreeZSaver : ZSave.ZSaver<UnityEngine.Tree> {
    public UnityEngine.ScriptableObject data;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TreeZSaver (UnityEngine.Tree Tree) : base(Tree.gameObject, Tree) {
        data = Tree.data;
        tag = Tree.tag;
        name = Tree.name;
        hideFlags = Tree.hideFlags;
    }
}
[System.Serializable]
public class TerrainColliderZSaver : ZSave.ZSaver<UnityEngine.TerrainCollider> {
    public UnityEngine.TerrainData terrainData;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TerrainColliderZSaver (UnityEngine.TerrainCollider TerrainCollider) : base(TerrainCollider.gameObject, TerrainCollider) {
        terrainData = TerrainCollider.terrainData;
        enabled = TerrainCollider.enabled;
        isTrigger = TerrainCollider.isTrigger;
        contactOffset = TerrainCollider.contactOffset;
        sharedMaterial = TerrainCollider.sharedMaterial;
        material = TerrainCollider.material;
        tag = TerrainCollider.tag;
        name = TerrainCollider.name;
        hideFlags = TerrainCollider.hideFlags;
    }
}
[System.Serializable]
public class TextMeshZSaver : ZSave.ZSaver<UnityEngine.TextMesh> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TextMeshZSaver (UnityEngine.TextMesh TextMesh) : base(TextMesh.gameObject, TextMesh) {
        text = TextMesh.text;
        font = TextMesh.font;
        fontSize = TextMesh.fontSize;
        fontStyle = TextMesh.fontStyle;
        offsetZ = TextMesh.offsetZ;
        alignment = TextMesh.alignment;
        anchor = TextMesh.anchor;
        characterSize = TextMesh.characterSize;
        lineSpacing = TextMesh.lineSpacing;
        tabSize = TextMesh.tabSize;
        richText = TextMesh.richText;
        color = TextMesh.color;
        tag = TextMesh.tag;
        name = TextMesh.name;
        hideFlags = TextMesh.hideFlags;
    }
}
[System.Serializable]
public class TilemapZSaver : ZSave.ZSaver<UnityEngine.Tilemaps.Tilemap> {
    public System.Single animationFrameRate;
    public UnityEngine.Color color;
    public UnityEngine.Vector3Int origin;
    public UnityEngine.Vector3Int size;
    public UnityEngine.Vector3 tileAnchor;
    public UnityEngine.Tilemaps.Tilemap.Orientation orientation;
    public UnityEngine.Matrix4x4 orientationMatrix;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TilemapZSaver (UnityEngine.Tilemaps.Tilemap Tilemap) : base(Tilemap.gameObject, Tilemap) {
        animationFrameRate = Tilemap.animationFrameRate;
        color = Tilemap.color;
        origin = Tilemap.origin;
        size = Tilemap.size;
        tileAnchor = Tilemap.tileAnchor;
        orientation = Tilemap.orientation;
        orientationMatrix = Tilemap.orientationMatrix;
        enabled = Tilemap.enabled;
        tag = Tilemap.tag;
        name = Tilemap.name;
        hideFlags = Tilemap.hideFlags;
    }
}
[System.Serializable]
public class TilemapRendererZSaver : ZSave.ZSaver<UnityEngine.Tilemaps.TilemapRenderer> {
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
    public UnityEngine.Material[] materials;
    public UnityEngine.Material material;
    public UnityEngine.Material sharedMaterial;
    public UnityEngine.Material[] sharedMaterials;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TilemapRendererZSaver (UnityEngine.Tilemaps.TilemapRenderer TilemapRenderer) : base(TilemapRenderer.gameObject, TilemapRenderer) {
        chunkSize = TilemapRenderer.chunkSize;
        chunkCullingBounds = TilemapRenderer.chunkCullingBounds;
        maxChunkCount = TilemapRenderer.maxChunkCount;
        maxFrameAge = TilemapRenderer.maxFrameAge;
        sortOrder = TilemapRenderer.sortOrder;
        mode = TilemapRenderer.mode;
        detectChunkCullingBounds = TilemapRenderer.detectChunkCullingBounds;
        maskInteraction = TilemapRenderer.maskInteraction;
        enabled = TilemapRenderer.enabled;
        shadowCastingMode = TilemapRenderer.shadowCastingMode;
        receiveShadows = TilemapRenderer.receiveShadows;
        forceRenderingOff = TilemapRenderer.forceRenderingOff;
        motionVectorGenerationMode = TilemapRenderer.motionVectorGenerationMode;
        lightProbeUsage = TilemapRenderer.lightProbeUsage;
        reflectionProbeUsage = TilemapRenderer.reflectionProbeUsage;
        renderingLayerMask = TilemapRenderer.renderingLayerMask;
        rendererPriority = TilemapRenderer.rendererPriority;
        rayTracingMode = TilemapRenderer.rayTracingMode;
        sortingLayerName = TilemapRenderer.sortingLayerName;
        sortingLayerID = TilemapRenderer.sortingLayerID;
        sortingOrder = TilemapRenderer.sortingOrder;
        allowOcclusionWhenDynamic = TilemapRenderer.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = TilemapRenderer.lightProbeProxyVolumeOverride;
        probeAnchor = TilemapRenderer.probeAnchor;
        lightmapIndex = TilemapRenderer.lightmapIndex;
        realtimeLightmapIndex = TilemapRenderer.realtimeLightmapIndex;
        lightmapScaleOffset = TilemapRenderer.lightmapScaleOffset;
        realtimeLightmapScaleOffset = TilemapRenderer.realtimeLightmapScaleOffset;
        materials = TilemapRenderer.materials;
        material = TilemapRenderer.material;
        sharedMaterial = TilemapRenderer.sharedMaterial;
        sharedMaterials = TilemapRenderer.sharedMaterials;
        tag = TilemapRenderer.tag;
        name = TilemapRenderer.name;
        hideFlags = TilemapRenderer.hideFlags;
    }
}
[System.Serializable]
public class TilemapCollider2DZSaver : ZSave.ZSaver<UnityEngine.Tilemaps.TilemapCollider2D> {
    public System.UInt32 maximumTileChangeCount;
    public System.Single extrusionFactor;
    public System.Single density;
    public System.Boolean isTrigger;
    public System.Boolean usedByEffector;
    public System.Boolean usedByComposite;
    public UnityEngine.Vector2 offset;
    public UnityEngine.PhysicsMaterial2D sharedMaterial;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public TilemapCollider2DZSaver (UnityEngine.Tilemaps.TilemapCollider2D TilemapCollider2D) : base(TilemapCollider2D.gameObject, TilemapCollider2D) {
        maximumTileChangeCount = TilemapCollider2D.maximumTileChangeCount;
        extrusionFactor = TilemapCollider2D.extrusionFactor;
        density = TilemapCollider2D.density;
        isTrigger = TilemapCollider2D.isTrigger;
        usedByEffector = TilemapCollider2D.usedByEffector;
        usedByComposite = TilemapCollider2D.usedByComposite;
        offset = TilemapCollider2D.offset;
        sharedMaterial = TilemapCollider2D.sharedMaterial;
        enabled = TilemapCollider2D.enabled;
        tag = TilemapCollider2D.tag;
        name = TilemapCollider2D.name;
        hideFlags = TilemapCollider2D.hideFlags;
    }
}
[System.Serializable]
public class CanvasGroupZSaver : ZSave.ZSaver<UnityEngine.CanvasGroup> {
    public System.Single alpha;
    public System.Boolean interactable;
    public System.Boolean blocksRaycasts;
    public System.Boolean ignoreParentGroups;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CanvasGroupZSaver (UnityEngine.CanvasGroup CanvasGroup) : base(CanvasGroup.gameObject, CanvasGroup) {
        alpha = CanvasGroup.alpha;
        interactable = CanvasGroup.interactable;
        blocksRaycasts = CanvasGroup.blocksRaycasts;
        ignoreParentGroups = CanvasGroup.ignoreParentGroups;
        enabled = CanvasGroup.enabled;
        tag = CanvasGroup.tag;
        name = CanvasGroup.name;
        hideFlags = CanvasGroup.hideFlags;
    }
}
[System.Serializable]
public class CanvasRendererZSaver : ZSave.ZSaver<UnityEngine.CanvasRenderer> {
    public System.Boolean hasPopInstruction;
    public System.Int32 materialCount;
    public System.Int32 popMaterialCount;
    public System.Boolean cullTransparentMesh;
    public System.Boolean cull;
    public UnityEngine.Vector2 clippingSoftness;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CanvasRendererZSaver (UnityEngine.CanvasRenderer CanvasRenderer) : base(CanvasRenderer.gameObject, CanvasRenderer) {
        hasPopInstruction = CanvasRenderer.hasPopInstruction;
        materialCount = CanvasRenderer.materialCount;
        popMaterialCount = CanvasRenderer.popMaterialCount;
        cullTransparentMesh = CanvasRenderer.cullTransparentMesh;
        cull = CanvasRenderer.cull;
        clippingSoftness = CanvasRenderer.clippingSoftness;
        tag = CanvasRenderer.tag;
        name = CanvasRenderer.name;
        hideFlags = CanvasRenderer.hideFlags;
    }
}
[System.Serializable]
public class CanvasZSaver : ZSave.ZSaver<UnityEngine.Canvas> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public CanvasZSaver (UnityEngine.Canvas Canvas) : base(Canvas.gameObject, Canvas) {
        renderMode = Canvas.renderMode;
        scaleFactor = Canvas.scaleFactor;
        referencePixelsPerUnit = Canvas.referencePixelsPerUnit;
        overridePixelPerfect = Canvas.overridePixelPerfect;
        pixelPerfect = Canvas.pixelPerfect;
        planeDistance = Canvas.planeDistance;
        overrideSorting = Canvas.overrideSorting;
        sortingOrder = Canvas.sortingOrder;
        targetDisplay = Canvas.targetDisplay;
        sortingLayerID = Canvas.sortingLayerID;
        additionalShaderChannels = Canvas.additionalShaderChannels;
        sortingLayerName = Canvas.sortingLayerName;
        worldCamera = Canvas.worldCamera;
        normalizedSortingGridSize = Canvas.normalizedSortingGridSize;
        enabled = Canvas.enabled;
        tag = Canvas.tag;
        name = Canvas.name;
        hideFlags = Canvas.hideFlags;
    }
}
[System.Serializable]
public class VisualEffectZSaver : ZSave.ZSaver<UnityEngine.VFX.VisualEffect> {
    public System.Boolean pause;
    public System.Single playRate;
    public System.UInt32 startSeed;
    public System.Boolean resetSeedOnPlay;
    public System.Int32 initialEventID;
    public System.String initialEventName;
    public UnityEngine.VFX.VisualEffectAsset visualEffectAsset;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public VisualEffectZSaver (UnityEngine.VFX.VisualEffect VisualEffect) : base(VisualEffect.gameObject, VisualEffect) {
        pause = VisualEffect.pause;
        playRate = VisualEffect.playRate;
        startSeed = VisualEffect.startSeed;
        resetSeedOnPlay = VisualEffect.resetSeedOnPlay;
        initialEventID = VisualEffect.initialEventID;
        initialEventName = VisualEffect.initialEventName;
        visualEffectAsset = VisualEffect.visualEffectAsset;
        enabled = VisualEffect.enabled;
        tag = VisualEffect.tag;
        name = VisualEffect.name;
        hideFlags = VisualEffect.hideFlags;
    }
}
[System.Serializable]
public class WheelColliderZSaver : ZSave.ZSaver<UnityEngine.WheelCollider> {
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
    public UnityEngine.PhysicMaterial sharedMaterial;
    public UnityEngine.PhysicMaterial material;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public WheelColliderZSaver (UnityEngine.WheelCollider WheelCollider) : base(WheelCollider.gameObject, WheelCollider) {
        center = WheelCollider.center;
        radius = WheelCollider.radius;
        suspensionDistance = WheelCollider.suspensionDistance;
        suspensionSpring = WheelCollider.suspensionSpring;
        suspensionExpansionLimited = WheelCollider.suspensionExpansionLimited;
        forceAppPointDistance = WheelCollider.forceAppPointDistance;
        mass = WheelCollider.mass;
        wheelDampingRate = WheelCollider.wheelDampingRate;
        forwardFriction = WheelCollider.forwardFriction;
        sidewaysFriction = WheelCollider.sidewaysFriction;
        motorTorque = WheelCollider.motorTorque;
        brakeTorque = WheelCollider.brakeTorque;
        steerAngle = WheelCollider.steerAngle;
        sprungMass = WheelCollider.sprungMass;
        enabled = WheelCollider.enabled;
        isTrigger = WheelCollider.isTrigger;
        contactOffset = WheelCollider.contactOffset;
        sharedMaterial = WheelCollider.sharedMaterial;
        material = WheelCollider.material;
        tag = WheelCollider.tag;
        name = WheelCollider.name;
        hideFlags = WheelCollider.hideFlags;
    }
}
[System.Serializable]
public class VideoPlayerZSaver : ZSave.ZSaver<UnityEngine.Video.VideoPlayer> {
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
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public VideoPlayerZSaver (UnityEngine.Video.VideoPlayer VideoPlayer) : base(VideoPlayer.gameObject, VideoPlayer) {
        source = VideoPlayer.source;
        url = VideoPlayer.url;
        clip = VideoPlayer.clip;
        renderMode = VideoPlayer.renderMode;
        targetCamera = VideoPlayer.targetCamera;
        targetTexture = VideoPlayer.targetTexture;
        targetMaterialRenderer = VideoPlayer.targetMaterialRenderer;
        targetMaterialProperty = VideoPlayer.targetMaterialProperty;
        aspectRatio = VideoPlayer.aspectRatio;
        targetCameraAlpha = VideoPlayer.targetCameraAlpha;
        targetCamera3DLayout = VideoPlayer.targetCamera3DLayout;
        waitForFirstFrame = VideoPlayer.waitForFirstFrame;
        playOnAwake = VideoPlayer.playOnAwake;
        time = VideoPlayer.time;
        frame = VideoPlayer.frame;
        playbackSpeed = VideoPlayer.playbackSpeed;
        isLooping = VideoPlayer.isLooping;
        timeSource = VideoPlayer.timeSource;
        timeReference = VideoPlayer.timeReference;
        externalReferenceTime = VideoPlayer.externalReferenceTime;
        skipOnDrop = VideoPlayer.skipOnDrop;
        controlledAudioTrackCount = VideoPlayer.controlledAudioTrackCount;
        audioOutputMode = VideoPlayer.audioOutputMode;
        sendFrameReadyEvents = VideoPlayer.sendFrameReadyEvents;
        enabled = VideoPlayer.enabled;
        tag = VideoPlayer.tag;
        name = VideoPlayer.name;
        hideFlags = VideoPlayer.hideFlags;
    }
}
[System.Serializable]
public class WindZoneZSaver : ZSave.ZSaver<UnityEngine.WindZone> {
    public UnityEngine.WindZoneMode mode;
    public System.Single radius;
    public System.Single windMain;
    public System.Single windTurbulence;
    public System.Single windPulseMagnitude;
    public System.Single windPulseFrequency;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public WindZoneZSaver (UnityEngine.WindZone WindZone) : base(WindZone.gameObject, WindZone) {
        mode = WindZone.mode;
        radius = WindZone.radius;
        windMain = WindZone.windMain;
        windTurbulence = WindZone.windTurbulence;
        windPulseMagnitude = WindZone.windPulseMagnitude;
        windPulseFrequency = WindZone.windPulseFrequency;
        tag = WindZone.tag;
        name = WindZone.name;
        hideFlags = WindZone.hideFlags;
    }
}
[System.Serializable]
public class PersistentGameObjectZSaver : ZSave.ZSaver<PersistentGameObject> {
    public System.Boolean useGUILayout;
    public System.Boolean enabled;
    public System.String tag;
    public System.String name;
    public UnityEngine.HideFlags hideFlags;
    public PersistentGameObjectZSaver (PersistentGameObject PersistentGameObject) : base(PersistentGameObject.gameObject, PersistentGameObject) {
        useGUILayout = PersistentGameObject.useGUILayout;
        enabled = PersistentGameObject.enabled;
        tag = PersistentGameObject.tag;
        name = PersistentGameObject.name;
        hideFlags = PersistentGameObject.hideFlags;
    }
}
