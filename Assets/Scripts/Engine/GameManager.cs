﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Game manager, implemented as a Singleton.
/// </summary>
public class GameManager : MonoBehaviour {

	private static GameManager _instance;

	[SerializeField]
	private TileMap _tileMap;

	// Controllers
	[SerializeField]
	private CameraController _cameraController;
	[SerializeField]
	private TurnOrderController _turnOrderController;
	[SerializeField]
	private MusicController _musicController;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static GameManager Instance { get { return _instance; } }

	/// <summary>
	/// Awake this instance and destroy duplicates.
	/// </summary>
	void Awake() {
		print ("GameManager.Awake()");
		if (_instance == null)
			_instance = this;
		else if (_instance != this)
			Destroy (this.gameObject);
		DontDestroyOnLoad (this.gameObject);
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		print ("GameManager.Start()");
		// Initialize camera controller
		_cameraController.Init (_tileMap.size_x * _tileMap.tileSize, _tileMap.size_z * _tileMap.tileSize, _tileMap.tileResolution);

		// Initialize tilemap
		_tileMap.Initialize ();
	}

	/// <summary>
	/// Gets the camera controller.
	/// </summary>
	/// <returns>The camera controller.</returns>
	public CameraController GetCameraController() {return _cameraController;}

	/// <summary>
	/// Gets the turn order controller.
	/// </summary>
	/// <returns>The turn order controller.</returns>
	public TurnOrderController GetTurnOrderController() {return _turnOrderController;}

	/// <summary>
	/// Gets the tile map.
	/// </summary>
	/// <returns>The tile map.</returns>
	public TileMap GetTileMap() {return _tileMap;}

	/// <summary>
	/// Gets the music controller.
	/// </summary>
	/// <returns>The music controller.</returns>
	public MusicController GetMusicController() {return _musicController;}
}