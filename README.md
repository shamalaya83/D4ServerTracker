# D3ServerStatus APP
Simple collaborative app to keep track of D3 servers status

![Screenshot of the app](https://github.com/shamalaya83/D3ServerStatus/blob/main/image.png)

The star symbol means that you have reviewed the server.  
The cross symbol means that the last rate is older than 3 days, so its better recheck the server.  
In brackets the review counter.  

bad => unplayable: server lags everywhere even in town or with few mobs  
laggy => lags but still playable: max 1 elite and few mobs around  
good => the server sometimes lags but you can definitely play on it: max 2 elites and mobs  
excellent => no lag: 3 or more elites and mobs  

# D3ServerStatus TurboHUD
Simple collaborative th plugin to keep track of D3 servers status

![Screenshot of the app](https://github.com/shamalaya83/D3ServerStatus/blob/main/ImmagineTH.png)

TH plugin [GameServerStatusPlugin.cs](https://github.com/shamalaya83/D3ServerStatus/blob/main/TurboHUD/GameServerStatusPlugin.cs) (Tier 3)  
Put in plugins/default  
logs are available in logs/GameServerStatusPlugin.txt  

The star symbol means that you have reviewed the server.  
The cross symbol means that the last rate is older than 3 days, so its better recheck the server.  
In brackets the review counter.  

Color:  
red -> bad  
yellow -> laggy  
green -> good  
purple -> excellent  

To rate the current server, type the following command in chat when in town:  
cmd: \s rating   where rating: 1 bad, 2, laggy, 3 good, 4 excellent  
Example: \s 3  

(N.B. if u write in group chat or in community ensure there are min 2 players)  
Join community on D3 "D3 Server Status" u can spam command there.  

