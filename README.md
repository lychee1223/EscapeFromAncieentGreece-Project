# 古代ギリシアからの脱出 \~アテナイの疫病編~
紀元前432年の古代ギリシアを舞台とした脱出ゲームのProjectデータです．<br>

## プレイする
- HDRP版はリポジトリ[EscapeFromAncieentGreece-Game](https://github.com/lychee1223/EscapeFromAncieentGreece-Game)からダウンロードをお願いします
- URP版は[UnityRoom](https://unityroom.com/games/escapefromancieentgreece)でWeb上から遊べます


## 開発環境
<table>
  <tr>
    <td> エンジン </td>
    <td> Unity 2021.3.8f </td>
  </tr>
  <tr>
    <td> 開発言語 </td>
    <td> C# </td>
  </tr>
  <tr>
    <td> パイプライン </td> 
    <td> HDRP<br>(※UnityRoom版はURPに変換しています) </td>
  </tr>
  <tr>
    <td> 開発期間 </td> 
    <td> 2023/03/15~2023/05/07 </td>
  </tr>
</table>


## 使用アセット
この作品の開発にあたり，以下のアセットを利用させて頂きました．この場をお借りして感謝申し上げます．
<!-- 建築 -->
- [Temple of Athena](https://assetstore.unity.com/packages/3d/environments/historic/temple-of-athena-86198)
- [Roman City Set](https://assetstore.unity.com/packages/3d/environments/historic/roman-city-set-168930)
- [Classic Columns](https://assetstore.unity.com/packages/3d/environments/historic/classic-columns-58062)
- [Creepy headless statue](https://assetstore.unity.com/packages/3d/props/exterior/creepy-headless-statue-196390)
- [Discobolus Statue](https://assetstore.unity.com/packages/3d/props/discobolus-statue-107544)
- [Greek Statue](https://assetstore.unity.com/packages/3d/props/greek-statue-190814)
<!-- 小道具 -->
- [Hoplite Shield (Aspis) Athens](https://assetstore.unity.com/packages/3d/props/weapons/hoplite-shield-aspis-athens-160185)
- [PBR Spartan Helmets](https://assetstore.unity.com/packages/3d/props/clothing/armor/pbr-spartan-helmets-230926)
- [PBR Kopis Swords](https://assetstore.unity.com/packages/3d/props/weapons/pbr-kopis-swords-231378)
- [Greek Temple: Vases](https://assetstore.unity.com/packages/3d/environments/historic/greek-temple-vases-149134)
- [Roman furniture: Roman villa pack](https://assetstore.unity.com/packages/3d/props/furniture/roman-furniture-roman-villa-pack-165586)
<!-- 環境 -->
- [Grass Flowers Pack Free](https://assetstore.unity.com/packages/2d/textures-materials/nature/grass-flowers-pack-free-138810)
- [VIS - PBR Grass Textures](https://assetstore.unity.com/packages/2d/textures-materials/floors/vis-pbr-grass-textures-198071)
- [Rock_Pack](https://assetstore.unity.com/packages/3d/environments/landscapes/rock-pack-210536)
- [Free Fire VFX - HDRP](https://assetstore.unity.com/packages/vfx/free-fire-vfx-hdrp-239742)
<!-- Material -->
- [World Materials Free](https://assetstore.unity.com/packages/2d/textures-materials/world-materials-free-150182)
- [Basic Metal Texture Pack](https://assetstore.unity.com/packages/2d/textures-materials/metals/basic-metal-texture-pack-37402)
<!-- Audio -->
- [Ancient Era Music Free Pack](https://assetstore.unity.com/packages/audio/music/ancient-era-music-free-pack-146823)
- [Western Audio & Music](https://assetstore.unity.com/packages/audio/sound-fx/western-audio-music-67788)
- [Coins Sound FX](https://assetstore.unity.com/packages/audio/sound-fx/foley/coins-sound-fx-29320)

> [!WARNING]
> 上記のアセットは本リポジトリに含まれていません．<br>
> したがって，**本リポジトリは完全なプロジェクトではなく，cloneするだけでは正常に動作はしません．<br>**
> また，一部のアセットはHDRP用にシェーダーをl変更しているため，**これらアセットをimportしても正常に動作しません**
> ご了承ください．


## 工夫点
> 詳細なソースコードはディレクトリ/Assets/Scripts/をご確認ください．

本ゲームにおいて，ユーザは左クリックのみでアイテムの取得や設置，扉の開錠など，様々なアクションを起こすことができます．<br>
本プロジェクトでは，同一インターフェースを実装した"Event"という単位でこれらのアクションを構築することで，極めて拡張性に富んだ設計を行っています．<br>
EventはEventListクラスでまとめて管理され，1つのEventListが1つのアクションを実装します．<br>
EventListクラスはIEventインターフェースを継承した様々なEventを1つのリストにまとめ，各要素のEventを逐次的に実行します．<br>
例えば，オブジェクトの移動と効果音の再生を逐次的に実行することで，扉の開閉といったアクションが実現されます．<br>
リストへの格納はインスペクタから容易に行うことが可能です．この設計により，インスペクタの操作のみで容易に様々なアクションの追加，調整を行うことができます．<br>

|Event名 | 内容 |
| --- | --- | 
| AddChangeCameraAngleEvent | カメラアングルの変更 |
| AddGetItemEvent | アイテムをインベントリへ追加 or 所持しているアイテムの入れ替え |
| AddGetDrachmaEvent | お金(ボーナスポイント)の取得 |
| AddMoveObjectEvent | 座標Aから座標B(または逆)へオブジェクトをt秒で移動させる |
| AddKeyInputEvent | 暗号システムへの入力を行う |
| AddSetActiveEvent | オブジェクトのアクティブ状態を設定する |
| AddMessageEvent | メッセージを表示する |
| AddGameOverEvent | リザルト画面を表示する |
| AddPlaySoundEvent | 効果音を鳴らす |
| AddSetStatusTagEventt | ヒントを管理するステータスタグを与える | 
