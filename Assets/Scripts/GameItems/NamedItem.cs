using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedItem : MonoBehaviour
{
    // Added this component to avoid some magic strings usage in code.
    // Used as Pool key or for Configs parsing.
    // TO REWIEVER:
    // If exists some better approach, please let me know. tnx =)
    [SerializeField]
    private string _name;
    public string definedName { get { return _name; } }
}
