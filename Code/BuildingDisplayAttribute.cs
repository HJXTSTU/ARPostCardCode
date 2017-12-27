using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDisplayAttribute {
	private Vector3 initLocalPosition;
	private Quaternion initLocalRotation;
	private Vector3 initLocalScale;

	public Vector3 InitLocalPosition{
		get{ 
			return initLocalPosition;
		}
		set{
			initLocalPosition = value;
		}
	}

	public Quaternion InitLocalRotation{
		get{ 
			return initLocalRotation;
		}
		set{ 
			initLocalRotation = value;
		}
	}

	public Vector3 InitLocalScale{
		get{ 
			return initLocalScale;
		}
		set{ 
			initLocalScale = value;
		}
	}
}
