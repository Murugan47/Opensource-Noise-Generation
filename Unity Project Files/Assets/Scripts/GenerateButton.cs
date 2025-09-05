using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GenerateButton : MonoBehaviour {

    public HexMeshCreator hexmeshcreator;

    [SerializeField] private Button GenerationButton = null;
	void Start()
    {
        GenerationButton.onClick.AddListener(hexmeshcreator.CallMap);
	}

}