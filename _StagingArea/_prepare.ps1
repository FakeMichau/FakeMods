Copy-Item ../BetterBossLootDropping/bin/Release/netstandard2.0/BetterBossLootDropping.dll         BetterBossLootDropping/
Copy-Item ../CheatersGoBrr/bin/Release/netstandard2.0/CheatersGoBrr.dll         CheatersGoBrr/
Copy-Item ../FuckBossShrines/bin/Release/netstandard2.0/FuckBossShrines.dll         FuckBossShrines/

Compress-Archive -Path BetterBossLootDropping/*     -Destination zips/BetterBossLootDropping.zip     -Force
Compress-Archive -Path CheatersGoBrr/*     -Destination zips/CheatersGoBrr.zip     -Force
Compress-Archive -Path FuckBossShrines/*     -Destination zips/FuckBossShrines.zip     -Force