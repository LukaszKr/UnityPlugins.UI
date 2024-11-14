using UnityPlugins.Common.Unity;
using UnityEngine;

namespace UnityPlugins.UI.Unity
{
	public class GUICanvas
	{
		public int Width;
		public int Height;
		public float Scale = 1f;

		public GUICanvas(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public void Begin()
		{
			int screenWidth = Screen.width;
			int screenHeight = Screen.height;

			float scaleX = screenWidth/(float)Width;
			float scaleY = screenHeight/(float)Height;
			Scale = Mathf.Min(scaleX, scaleY);
			Vector3 scaleVec = new Vector3(Scale, Scale, 1f);

			//calculate position to keep canvas in center
			Vector3 positionVec = new Vector3(0, 0, 0);
			if(scaleX < scaleY)
			{
				float heightGap = screenHeight-(Height*scaleX);
				positionVec.y = heightGap/2f;
			}
			else
			{
				float widthGap = screenWidth-(Width*scaleY);
				positionVec.x = widthGap/2f;
			}

			Matrix4x4 matrix = Matrix4x4.TRS(positionVec, Quaternion.identity, scaleVec);
			GUIExt.PushMatrix(matrix);
		}

		public void End()
		{
			GUIExt.PopMatrix();
		}
	}
}
