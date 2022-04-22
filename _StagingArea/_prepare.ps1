Copy-Item ../BossLootIntoInventory/bin/Release/netstandard2.0/BossLootIntoInventory.dll         BossLootIntoInventory/
Copy-Item ../CheatersGoBrr/bin/Release/netstandard2.0/CheatersGoBrr.dll         CheatersGoBrr/
Copy-Item ../FuckBossShrines/bin/Release/netstandard2.0/FuckBossShrines.dll         FuckBossShrines/

Compress-Archive -Path BossLootIntoInventory/*     -Destination zips/BossLootIntoInventory.zip     -Force
Compress-Archive -Path CheatersGoBrr/*     -Destination zips/CheatersGoBrr.zip     -Force
Compress-Archive -Path FuckBossShrines/*     -Destination zips/FuckBossShrines.zip     -Force