using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class CustomHingeSerializer : MonoBehaviour
{
    public static void SerializeJoint(ZSerializer.Internal.ZSerializer serializer, Component component)
    {
        Debug.LogWarning("<color=yellow>SerializeJoint has been disabled prior to the building of the Unity Component's ZSerializers. Please uncomment these lines for the serializer to work.</color>");
        // var hinge = (HingeJoint2D)component;
        // var zserializer = (HingeJoint2DZSerializer)serializer;
        // zserializer.serializableLimits = new Vector2(hinge.limits.min, hinge.limits.max);
        // zserializer.serializableMotor = new Vector2(hinge.motor.motorSpeed, hinge.motor.maxMotorTorque);
    }

    public static void DeserializeJoint(ZSerializer.Internal.ZSerializer serializer, Component component)
    {
        Debug.LogWarning("<color=yellow>DeserializeJoint has been disabled prior to the building of the Unity Component's ZSerializers. Please uncomment these lines for the serializer to work.</color>");

        
        // var hinge = (HingeJoint2D)component;
        // var hingeSerializer = (HingeJoint2DZSerializer)serializer;
        //
        // hinge.limits = new JointAngleLimits2D
        // {
        //     min = hingeSerializer.serializableLimits.x,
        //     max = hingeSerializer.serializableLimits.y
        // };
        //
        // hinge.motor = new JointMotor2D
        // {
        //     motorSpeed = hingeSerializer.serializableMotor.x,
        //     maxMotorTorque = hingeSerializer.serializableMotor.y
        // };
        //
        // hinge.useLimits = hingeSerializer.useLimits;
        // hinge.useMotor = hingeSerializer.useMotor;
    }
}