# LivesLivesLog
LivesLivesLog is remote Log tool for unity,for pc or mobile.

# Start Server

first open LivesLivesLog App Window:

![image](https://github.com/ThisisGame/LivesLivesLog/blob/master/doc/menu.png)

![image](https://github.com/ThisisGame/LivesLivesLog/blob/master/doc/logwindowempty.png)

you must set the ip address to your LAN address.
![image](https://github.com/ThisisGame/LivesLivesLog/blob/master/doc/LAN.png)


# Start Client

then call LivesLivesLog Init in your unity project:
```
LivesLivesLog.GetSingleton().Init();
```

as LivesLivesLog is read ip address from config file,

you need create LivesLivesLog.txt in the path:
```
Application.persistentDataPath + "/LivesLivesLog.txt"
```

and write you compute ip address.

![image](https://github.com/ThisisGame/LivesLivesLog/blob/master/doc/livesliveslogconfig.png)


# Run

![image](https://github.com/ThisisGame/LivesLivesLog/blob/master/doc/logwindow.png)
