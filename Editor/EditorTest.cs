using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class EditorTest : EditorWindow 
{
	[MenuItem("Window/EditorTest")]
	public static void ShowWindow()
	{
		EditorWindow hh = EditorWindow.GetWindow(typeof(EditorTest));
		hh.wantsMouseMove = true;
	}

	Rect buttonRect = new Rect(0, 0, 100, 20);
	bool dragging = false;

	private Texture2D image = Resources.Load<Texture2D>("right");
	private Texture2D image2 = Resources.Load<Texture2D>("down");
	private Texture2D rectangle = CreateRectangle();

	Vector2 mousePos = new Vector2(0,0);

	private float lineHeight = 15;

	EditorTest()
	{
		//image = Resources.Load<Texture2D> ("right");
	}

	static Texture2D CreateRectangle()
	{
		Texture2D tex = new Texture2D(1,1);
		tex.SetPixel (0,0, Color.red);
		tex.Apply ();
		return tex;
	}

	bool open = false;

	public const int INDENT_PIXELS = 16;
	public const int LINE_HEIGHT_PIXELS = 16; 

	public class TreeItem
	{
		public Texture2D icon;
		public string text;
		public object value;
		public bool expanded;
		public TreeItem parent;
		public List<TreeItem> children;

		public void Expand()
		{
			expanded = true;
		}
		public void Collapse()
		{
			expanded = false;
		}
	}

	public TreeItem tree = TreeInitialize();

	static TreeItem TreeInitialize()
	{
		TreeItem root = new TreeItem();
		root.text = "Root";
		root.icon = Resources.Load<Texture2D>("cog");
		root.children = new List<TreeItem>();

		TreeItem kk1 = new TreeItem();
		kk1.text = "Some condition 2";
		kk1.icon = Resources.Load<Texture2D>("question");
		root.children.Add (kk1);

		TreeItem kk2 = new TreeItem();
		kk2.text = "Something else";
		kk2.icon = Resources.Load<Texture2D>("arrow_divide");
		root.children.Add (kk2);
		kk2.children = new List<TreeItem>();
		kk2.children.Add (kk1);
		return root;
	}

	void OnGUI()
	{
		DrawTreeItem(0, 0, tree);

		/*
		//Debug.Log (Event.current.type.ToString() + System.DateTime.Now);

		//Debug.Log(Event.current.type);
//		if (buttonRect.Contains(Event.current.mousePosition))
//		{
//			if (Event.current.type == EventType.MouseDown)
//				dragging = true;
//			else if (Event.current.type == EventType.MouseUp)
//				dragging = false;
//		}
//
//		if (dragging && Event.current.type == EventType.MouseDrag)
//		{
//			buttonRect.x += Event.current.delta.x;
//			buttonRect.y += Event.current.delta.y;
//		}
		if (Event.current.type == EventType.MouseMove)
		{
			mousePos = Event.current.mousePosition;
			Event.current.Use ();
		}


		int lineMouseOver = (int)(mousePos.y / lineHeight);
		//GUI.Box (new Rect(0, lineMouseOver*lineHeight, position.width, lineHeight), 
		GUI.DrawTexture(new Rect(0, lineMouseOver*lineHeight, position.width, lineHeight), rectangle);

		//GUI.Button (buttonRect, "Drag me");


		if (Event.current.type == EventType.MouseDown)
		{
			open = !open;
			Event.current.Use();
		}

		TreeLeaf (0, 0, "Line 1");
		TreeLeaf (1, 0, "Line 2");

		GUI.Button (new Rect(0,40, 100, 20), "Test");
		*/
	}

	void DrawTreeItem(int line, int indent, TreeItem item)
	{
		float indentOffset = indent * INDENT_PIXELS;
		float lineOffset = line * LINE_HEIGHT_PIXELS;
		Rect r = new Rect(indentOffset, lineOffset, LINE_HEIGHT_PIXELS, LINE_HEIGHT_PIXELS);
		if (item.children != null && item.children.Count > 0)
		{
			if (Event.current.type == EventType.MouseDown && r.Contains(Event.current.mousePosition))
			{
				item.expanded = !item.expanded;
				Event.current.Use();
			}

			GUI.DrawTexture(r, item.expanded ? image2 : image);
			r.x += LINE_HEIGHT_PIXELS;
		}
		if (item.icon != null)
		{
			GUI.DrawTexture(r, item.icon);
			r.x += LINE_HEIGHT_PIXELS;
		}
		r.width = position.width - r.x;
		GUI.Label(r, item.text);

		if (item.expanded && item.children != null)
		{
			int nChildren = item.children.Count;
			for (int i=0; i<nChildren; ++i)
			{
				DrawTreeItem(++line, indent+1, item.children[i]);
			}
		}
	}
}
