using System;
using UnityEngine;
namespace AssemblyCSharp
{
	public class MapsUtil
	{
		public static void PlanWalkRouteInAndroid(BuildingInfo info){
			AndroidJavaClass jclass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject activity = jclass.GetStatic<AndroidJavaObject>("currentActivity");

			AndroidJavaClass mapPlugin = new AndroidJavaClass ("com.wwt.AR.OpenMap");
			mapPlugin.CallStatic("openMap",activity,info.lat,info.lon,info.name);
		}

	}
}

