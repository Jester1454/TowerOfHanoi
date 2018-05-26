using System;
using System.Collections;
using Boo.Lang;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	private int diskCount;
	private bool resolveFinished = false;
	private List<Disk> listDisks = new List<Disk>();
	
	public int MaxDiskCount;
	
	public Disk DiskPrefab;
	public Tower StartTower;
	public Tower MiddleTower;
	public Tower FinishTower;
	public GameObject ChooseCoundDiskPanel;
	public GameObject FinishPanel;
	public InputField CountDiskField;
	
	private void Awake()
	{
		ChooseCoundDiskPanel.SetActive(!resolveFinished);
		FinishPanel.SetActive(resolveFinished);		
	}

	private void InitTowers()
	{	
		StartTower.Initialization(diskCount);
		MiddleTower.Initialization(diskCount);
		FinishTower.Initialization(diskCount);
	}

	private void CreateStartedDisk()
	{
		for (int i = 0; i < listDisks.Count; i++)
		{
			Destroy(listDisks[i].gameObject);
		}

		listDisks.Clear();;
		
		for (int i = 0; i < diskCount; i++)
		{
			Disk disk = Instantiate(DiskPrefab);
			disk.Initialization(i, StartTower);
			StartTower.AddDisk(disk);
			listDisks.Add(disk);
		}
	}

	public void StartResolve()
	{
		diskCount = Convert.ToInt32(CountDiskField.text);

		if (diskCount <= MaxDiskCount)
		{
			ChooseCoundDiskPanel.SetActive(resolveFinished);
			InitTowers();
			CreateStartedDisk();
			StartCoroutine(StartResolveCoroutine());
		}
		else
		{
			CountDiskField.text = "Превышено максимальное число дисков";
		}
	}

	private IEnumerator StartResolveCoroutine()
	{
		yield return StartCoroutine(SolutionHanoibns(diskCount, StartTower, FinishTower, MiddleTower));
		
		resolveFinished = true;
		FinishPanel.SetActive(resolveFinished);
	}

	private IEnumerator SolutionHanoibns(int counDisk, Tower start, Tower middle, Tower finish)
	{
		if (counDisk > 1)
		{
			yield return SolutionHanoibns(counDisk - 1, start, finish, middle);
		}

		yield return start.MoveDisk(middle);
		
		if (counDisk > 1)
		{
			yield return SolutionHanoibns(counDisk-1, finish, middle, start);
		}
	}

	public void Retry()
	{
		resolveFinished = false;
		
		FinishPanel.SetActive(resolveFinished);
		ChooseCoundDiskPanel.SetActive(!resolveFinished);
	}
}
