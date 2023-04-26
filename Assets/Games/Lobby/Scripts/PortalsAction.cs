using EasyRoads3Dv3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalsAction : MonoBehaviour
{
    [SerializeField] private GameObject portalBlockParty;
    [SerializeField] private GameObject portalDoll;
    [SerializeField] private GameObject portalRace;
    [SerializeField] private GameObject portalUltimateKnockout;

    void Start()
    {
        Rigidbody rigidbodyBlockParty = portalBlockParty.AddComponent<Rigidbody>();
        rigidbodyBlockParty.useGravity = false;
        rigidbodyBlockParty.isKinematic = true;

        BoxCollider colliderBlockParty = portalBlockParty.AddComponent<BoxCollider>();
        BoxCollider colliderDoll = portalDoll.AddComponent<BoxCollider>();
        BoxCollider colliderRace = portalRace.AddComponent<BoxCollider>();
        BoxCollider colliderUltimateKnockout = portalUltimateKnockout.AddComponent<BoxCollider>();

        colliderBlockParty.isTrigger = true;
        colliderDoll.isTrigger = true;
        colliderRace.isTrigger = true;
        colliderUltimateKnockout.isTrigger = true;


        //TriggerAction triggerForBlockParty = new TriggerAction("BlockParty");
        TriggerAction triggerForBlockParty = portalBlockParty.AddComponent<TriggerAction>();
        triggerForBlockParty.NameGame = "BlockParty";

        TriggerAction triggerForDoll = portalDoll.AddComponent<TriggerAction>();
        triggerForDoll.NameGame = "Doll";

        TriggerAction triggerForRace = portalRace.AddComponent<TriggerAction>();
        triggerForRace.NameGame = "Track";

        TriggerAction triggerForUltimateKnockout = portalUltimateKnockout.AddComponent<TriggerAction>();
        triggerForUltimateKnockout.NameGame = "UltimateKnockout";
    }

}
