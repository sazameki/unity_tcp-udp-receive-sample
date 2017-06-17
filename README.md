# unity_tcp-udp-receive-sample

Unity上でTCP/IPおよびUDP/IPのメッセージを受信するサンプルプログラムです。
TCP/IPやUDP/IPのメッセージを受信するたびに、キューブが回転します。

![スクリーンショット](http://sazameki.jp/temp/unity_tcp-udp-receive-sample_screenshot.png)

TCP/IPの受信コア部はTCPReceiver.cs、UDP/IPの受信コア部はUDPReceiver.csです。それぞれのスクリプトをEmpty Objectに追加し、ポート番号を変更することで、各種サーバを起動して利用できます。デフォルトのポート番号はそれぞれTCPが12345、UDPが22222に設定されていますが、Unityエディタのインスペクタから変更できます。

受信したメッセージの管理部分については、GameManager.csを参照してください。[MethodImpl(MethodImplOptions.Synchronized)] というアノテーションを使用した同期手法を使用して、メインスレッドとTCP/IPおよびUDP/IPの受信用スレッドの両方からメッセージ格納用のキューにアクセスしています。
