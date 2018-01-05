package com.wwt.AR;

import com.unity3d.player.UnityPlayerActivity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;

public class MainActivity extends UnityPlayerActivity {

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_main);
		PackageManagerUtil.setContext(this.getApplicationContext());
	}


	public void Route(double dlat,double dlon,String dname) {
		Intent intent = new Intent();  
		intent.setAction(Intent.ACTION_VIEW);  
		intent.addCategory(Intent.CATEGORY_DEFAULT);  
		//29.591958,106.3018820000
		//String url = "androidamap://route?sourceApplication=amap"+"&sname="+"我的位置" +"&dlat="+29.5919580000+"&dlon="+106.3018820000+"&dname="+"虎溪图书馆"+"&dev=0&t=0";  
		String url = getUri("amap",dlat,dlon,dname);
		Uri uri = Uri.parse(url);  
		//将功能Scheme以URI的方式传入data  
		intent.setData(uri);  
		//启动该页面即可  
		startActivity(intent);
	}

//	private void openGaodeMapToGuide() {  
//		Intent intent = new Intent();  
//		intent.setAction(Intent.ACTION_VIEW);  
//		intent.addCategory(Intent.CATEGORY_DEFAULT);  
//		//String url = "androidamap://route?sourceApplication=amap"+"&sname="+"我的位置" +"&dlat="+29.5919580000+"&dlon="+106.3018820000+"&dname="+"虎溪图书馆"+"&dev=0&t=0";  
//		String url = getUri("amap",29.591958,106.3018820000,"虎溪图书馆");
//		Uri uri = Uri.parse(url);  
//		//将功能Scheme以URI的方式传入data  
//		intent.setData(uri);  
//		//启动该页面即可  
//		startActivity(intent);  
//	}  

	private String getUri(String sourceApplication,double dlat,double dlon,String dname) {
		String sname = "我的位置";
		int t=2;
		int dev = 0;
		StringBuffer res=new StringBuffer();
		res.append("amapuri://route/plan/?");
		uriAppend(res,"sourceApplication",sourceApplication,true);
		uriAppend(res,"dlat",Double.toString(dlat),true);
		uriAppend(res,"dlon",Double.toString(dlon),true);
		uriAppend(res,"dname",dname,true);
		uriAppend(res,"sname",sname,true);
		uriAppend(res,"t",Integer.toString(t),true);
		uriAppend(res,"dev",Integer.toString(dev),false);

		return res.toString();
	}

	private void uriAppend(StringBuffer sb,String paramName,String param,boolean hasNext) {
		sb.append(paramName+"="+param);
		if(hasNext){
			sb.append("&");
		}
	}
}
