using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
	[Header("Player")]
	public bool canPlayerEnter;
	public bool canPlayerExit;

	[Space]

	[Header("Enemy")]
	public bool canEnemyEnter;
	public bool canEnemyExit;
}
