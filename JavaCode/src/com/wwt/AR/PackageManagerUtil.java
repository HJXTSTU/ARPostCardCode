package com.wwt.AR;

import java.util.ArrayList;
import java.util.List;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;

public class PackageManagerUtil {

	private static Context mContext=null;
	private static PackageManager mPackageManager =null;  
	private static List<String> mPackageNames = new ArrayList<String>();  
	private static final String GAODE_PACKAGE_NAME = "com.autonavi.minimap";  
	private static final String BAIDU_PACKAGE_NAME = "com.baidu.BaiduMap";  
	private static void initPackageManager(){  

		mPackageManager=mContext.getPackageManager();
		List<PackageInfo> packageInfos = mPackageManager.getInstalledPackages(0);  

		if (packageInfos != null) {  
			for (int i = 0; i < packageInfos.size(); i++) {  
				mPackageNames.add(packageInfos.get(i).packageName);  
			}  
		}  
	}  

	public static void setContext(Context context) {
		mContext=context;
	}

	public static boolean haveGaodeMap(){  
		initPackageManager();  
		return mPackageNames.contains(GAODE_PACKAGE_NAME);  
	}  

	public static boolean haveBaiduMap(){  
		initPackageManager();  
		return mPackageNames.contains(BAIDU_PACKAGE_NAME);  
	}  
}
