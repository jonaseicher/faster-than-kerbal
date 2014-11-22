using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UKEditorProgressBar {
	public class ProgressInfo {
		public string Text;
		public int MaxSteps;
		public int Step;

		public float Percentage {
			get { 
				if (MaxSteps == 0) return 0f;
				else return Mathf.Clamp01((float)(Step-1) / (float)MaxSteps);
			}
		}

		public float PercentagePart {
			get {
				if (MaxSteps == 0) return 0f;
				else return 1f / (float)MaxSteps;
			}
		}

		public void Next(string text) {
			++Step;
			Text = text;
		}

		public override string ToString ()
		{
			return string.Format ("[ProgressInfo: Percentage={0}, PercentagePart={1}]", Percentage, PercentagePart);
		}
	}

	public string Title;

	public double LastUpdateTime = 0f;
	public double UpdateTimeout = 1f;

	public List<ProgressInfo> ProgressStack;

	public UKEditorProgressBar(string title) {
		Title = title;
		ProgressStack = new List<ProgressInfo>();
	}

	public string Text() {
		StringBuilder sb = new StringBuilder();

		foreach(var p in ProgressStack) {
			sb.Append(" / ");
			sb.Append(p.Text);
		}

		return sb.ToString();
	}

	private float LayerMaxSize(int layer) {
		if (layer <= 0) return 1f;
		else {
			return LayerMaxSize(layer - 1) * ProgressStack[layer - 1].PercentagePart;
		}
	}

	public float Progress() {
		if (ProgressStack.Count == 0) return 0f;
		else if (ProgressStack.Count == 1) return ProgressStack[0].Percentage;
		else {
			float sum = 0f;

			for(int layer = 0; layer < ProgressStack.Count; ++layer) {
				sum += ProgressStack[layer].Percentage * LayerMaxSize(layer);
			}

			return sum;
		}
	}

	public void Update() {
		if (EditorApplication.timeSinceStartup - LastUpdateTime > UpdateTimeout) {
			LastUpdateTime = EditorApplication.timeSinceStartup;
			EditorUtility.DisplayProgressBar(Title, Text(), Progress());
		}

		/*
		Debug.Log(Progress());
		for(int layer = 0; layer < ProgressStack.Count; ++layer) {
			Debug.Log(ProgressStack[layer]);
		}
		*/
	}

	public void Begin(int maxSteps) {
		ProgressStack.Add(new ProgressInfo(){
			MaxSteps = maxSteps,
			Text = "",
			Step = 0,
		});
		Update();
	}

	public void End() {
		ProgressStack.RemoveAt(ProgressStack.Count - 1);
		if (ProgressStack.Count == 0) EditorUtility.ClearProgressBar();
	}

	public void Next(string text) {
		ProgressStack[ProgressStack.Count - 1].Next(text);
		Update();
	}

	// -------------------------

	private static UKEditorProgressBar TheBar = null;

	public static void TheBarPrepare(string title) {
		if (TheBar == null) {
			EditorUtility.ClearProgressBar();
			TheBar = new UKEditorProgressBar(title);
		}
	}

	public static void TheBarBegin(int maxSteps) {
		if (TheBar != null) TheBar.Begin(maxSteps);
	}

	public static void TheBarEnd() {
		if (TheBar != null) TheBar.End();
		if (TheBar.ProgressStack.Count == 0) TheBar = null;
	}

	public static void TheBarNext(string text) {
		if (TheBar != null) TheBar.Next(text);
	}
}
