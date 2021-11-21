ALTER TABLE tv ALTER COLUMN url NVARCHAR(2000) not null

UPDATE tv SET
url='<object width="100%" height="100%"><param name="movie" value="http://rt.com/s/swf/player.swf?overstretch=true&amp;showdigits=false&amp;skin=http://developer.longtailvideo.com/trac/changeset/643/skins/beelden?old_path=%2F&amp;format=zip&amp;streamer=rtmp://fms5.visionip.tv/live&amp;type=rtmp&amp;abouttext=Russia Today&amp;aboutlink=http://rt.com&amp;autostart=true&amp;file=RT_2.flv">
<embed src="http://rt.com/s/swf/player.swf?overstretch=true&amp;showdigits=false&amp;skin=http://developer.longtailvideo.com/trac/changeset/643/skins/beelden?old_path=%2F&amp;format=zip&amp;streamer=rtmp://fms5.visionip.tv/live&amp;type=rtmp&amp;abouttext=Russia Today&amp;aboutlink=http://rt.com&amp;autostart=true&amp;file=RT_2.flv" type="application/x-shockwave-flash" allowfullscreen="true" width="100%" height="100%"></object>'
WHERE id=1

UPDATE tv SET
url='<iframe src="http://www.byutv.org/watch/livetv" width="100%" height="100%" />'
WHERE id=2

UPDATE tv SET
url='<embed name="cspan-video-player" src="http://d1k4es7bw1lvxt.cloudfront.net/cspanPlayer.php" allowscriptaccess="always" bgcolor="#ffffff" quality="high" allowfullscreen="true" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="autoCC=true&amp;system=http://d1k4es7bw1lvxt.cloudfront.net/flashXml.php?live=3&amp;autoplay=true" align="middle" width="100%" height="100%">'
WHERE id=3

UPDATE tv SET
url='<embed name="cspan-video-player" src="http://d1k4es7bw1lvxt.cloudfront.net/cspanPlayer.php" allowscriptaccess="always" bgcolor="#ffffff" quality="high" allowfullscreen="true" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="autoCC=true&amp;system=http://d1k4es7bw1lvxt.cloudfront.net/flashXml.php?live=1&amp;autoplay=true" align="middle" width="100%" height="100%">'
WHERE id=4

DELETE tv_language WHERE tv_id=5
DELETE tv WHERE id=5

SET IDENTITY_INSERT tv ON
INSERT INTO tv
(id, image_url, url, language_id)
SELECT 5, image_url,
'<embed name="cspan-video-player" src="http://d1k4es7bw1lvxt.cloudfront.net/cspanPlayer.php" allowscriptaccess="always" bgcolor="#ffffff" quality="high" allowfullscreen="true" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" flashvars="autoCC=true&amp;system=http://d1k4es7bw1lvxt.cloudfront.net/flashXml.php?live=2&amp;autoplay=true" align="middle" width="100%" height="100%">',
language_id
FROM tv
WHERE id=4
SET IDENTITY_INSERT tv OFF

INSERT INTO tv_language
(tv_id, language_id, name, description)
SELECT 5, language_id, 'CSPAN2', description
FROM tv_language
WHERE tv_id=4

UPDATE tv SET
url='<object type="application/x-shockwave-flash" data="http://player.sharp-stream.com/player.swf" width="100%" height="100%" bgcolor="#000000" id="mediaplayer1" name="mediaplayer1" tabindex="0"><param name="allowfullscreen" value="true"><param name="allowscriptaccess" value="always"><param name="seamlesstabbing" value="true"><param name="wmode" value="opaque"><param name="flashvars" value="netstreambasepath=http%3A%2F%2Fwww.islamchannel.tv%2F&amp;id=mediaplayer1&amp;className=stream_player&amp;autostart=true&amp;stretching=exactfit&amp;provider=rtmp&amp;rtmp.loadbalance=true&amp;file=http%3A%2F%2Ftx.sharp-stream.com%2Fjw.php%3Fc%3Dislamtv%2Fislamtv&amp;controlbar.position=over"></object>'
WHERE id=6

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="http://www.jc-tv.net/watchlive/jc-tv400.asx">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="http://www.jc-tv.net/watchlive/jc-tv400.asx" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=7

DELETE tv_language WHERE tv_id=8
DELETE tv WHERE id=8

DELETE tv_language WHERE tv_id=9
DELETE tv WHERE id=9

DELETE tv_language WHERE tv_id=10
DELETE tv WHERE id=10

UPDATE tv SET
url='<iframe src="http://www.pentagonchannel.mil/LiveStream.aspx" width="100%" height="100%" />'
WHERE id=11

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="mms://live1.wm.skynews.servecast.net/skynews_wmlz_live300k">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="mms://live1.wm.skynews.servecast.net/skynews_wmlz_live300k" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=12

UPDATE tv SET
url='<iframe src="http://ibnlive.in.com/livetv/" width="100%" height="100%" />'
WHERE id=13

UPDATE tv SET
url='<object type="application/x-shockwave-flash" id="cvp_1" name="cvp_1" data="http://i.cdn.turner.com/cnn/.element/apps/cvp/3.0/swf/cnn_video.swf" width="100%" height="100%"><param name="quality" value="high"><param name="bgcolor" value="#000000"><param name="allowFullScreen" value="true"><param name="allowScriptAccess" value="always"><param name="wmode" value="transparent"><param name="flashvars" value="contentId=world/2013/02/02/nr-weekend-lemon-robertson-turkey.cnn&amp;context=640x406_start_bvp_edition&amp;domId=cvp_1&amp;w=640&amp;h=406"></object>'
WHERE id=14

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="http://www.bloomberg.com/streams/video/LiveBTV56.asxx">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="http://www.bloomberg.com/streams/video/LiveBTV56.asxx" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=15

UPDATE tv SET
url='<object type="application/x-shockwave-flash" data="http://c.brightcove.com/services/viewer/federated_f9?&amp;width=480&amp;height=360&amp;flashID=myExperience1663542797001&amp;bgcolor=%23FFFFFF&amp;playerID=1654994530001&amp;playerKey=AQ~~%2CAAABIV9FIvk~%2C6co39pxPTViq7B4nCG5Oq8Fc_mzILSO9&amp;isVid=true&amp;isUI=true&amp;dynamicStreaming=true&amp;wmode=opaque&amp;htmlFallback=true&amp;%40videoPlayer=1663542797001&amp;autoStart=&amp;debuggerID=&amp;startTime=1359909285931" id="myExperience1663542797001" width="100%" height="100%" class="BrightcoveExperience" seamlesstabbing="undefined"><param name="allowScriptAccess" value="always"><param name="allowFullScreen" value="true"><param name="seamlessTabbing" value="false"><param name="swliveconnect" value="true"><param name="wmode" value="opaque"><param name="quality" value="high"><param name="bgcolor" value="#FFFFFF"></object>'
WHERE id=16

DELETE tv_language WHERE tv_id=17
DELETE tv WHERE id=17

DELETE tv_language WHERE tv_id=18
DELETE tv WHERE id=18

--- Russian TV ---
UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="mms://213.232.226.11/MIRTV_300">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="mms://213.232.226.11/MIRTV_300" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=24

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="mms://mms.amtv.ru/BizOne">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="mms://mms.amtv.ru/BizOne" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=29

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="http://video.rbc.ru/top/news.wmv">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="http://video.rbc.ru/top/news.wmv" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=30

UPDATE tv SET
url='<object id="MediaPlayer" type="application/x-oleobject" height="100%" width="100%" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6">
<param name="URL" value="mms://live.rfn.ru/rtr-planeta">
<param name="SendPlayStateChangeEvents" value="True">
<param name="AutoStart" value="True">
<param name="uiMode" value="mini">
<param name="PlayCount" value="9999">
<param name="stretchToFit" value="True">
<param name="enableContextMenu" value="False">
<embed type="application/x-mplayer2" pluginspage="http://www.microsoft.com/downloads/details.aspx?FamilyID=9194bb10-00ea-4500-859d-9a319738d4d8&amp;DisplayLang=en/" src="mms://live.rfn.ru/rtr-planeta" name="player" showcontrols="1" showdisplay="0" showstatusbar="1" animationatstart="1" transparentatstart="0" allowchangedisplaysize="1" autosize="0" displaysize="0" enablecontextmenu="0" windowless="1" width="100%" height="100%" enablefullscreencontrols="1"></object>'
WHERE id=31

--
UPDATE tv SET
url='<embed width="100%" height="100%" allowfullscreen="true" allowscriptaccess="always" type="application/x-shockwave-flash" src="http://pro-tv.net/player/player.swf?file=one&amp;type=rtmp&amp;streamer=rtmp://178.218.214.74:1935/live/&amp;displayclick=fullscreen&amp;autostart=true&amp;stretching=exactfit&amp;skin=http://pro-tv.net/player/jwskins/slim.zip">'
WHERE id=20

UPDATE tv SET
url='<object type="application/x-oleobject" classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95" width="100%" height="100%">
<param name="FileName" value="mms://cdnvideowmlive.fplive.net/cdnvideowmlive-live/r24_hq">
<param name="wmode" value="transparent">
<param name="AutoStart" value="true">
<param name="stretchToFit" value="true">
<param name="enableContextMenu" value="true">
<param name="ShowStatusBar" value="1">
<embed src="mms://cdnvideowmlive.fplive.net/cdnvideowmlive-live/r24_hq" type="application/x-mplayer2" autostart="1" stretchtofit="1" enablecontextmenu="0" wmode="transparent" showstatusbar="1" width="100%" height="100%"> </object>'
WHERE id=21

UPDATE tv SET
url='<iframe src="http://lapti.tv/rossiya-2-online.html" width="100%" height="100%" />'
WHERE id=22

UPDATE tv SET
url='<object width="100%" height="100%"><param value="http://smotriru.com/player/liveplayer.swf" name="data">
<param value="true" name="allowFullScreen"><param value="always" name="allowScriptAccess"><param value="file=http://85.132.71.4:1935/turktv/ntvrussia.sdp/playlist.f4m" name="flashvars"><embed width="100%" height="100%" flashvars="file=http://85.132.71.4:1935/turktv/ntvrussia.sdp/playlist.f4m&amp;noLogo=true&amp;adv=false" allowscriptaccess="always" allowfullscreen="true" wmode="black" type="application/x-shockwave-flash" src="http://smotriru.com/player/liveplayer.swf"></object>'
WHERE id=23

UPDATE tv SET
url='<object width="100%" height="100%"><param name="allowFullScreen" value="true"><param name="wmode" value="opaque"><param name="movie" value="http://www.mirtv.ru/media/flash/player.swf?videoUrl=rtmp://mirtv.cdnvideo.ru/mirtv-live/mirtv600.sdp&amp;live=true&amp;emb=true"><embed src="http://www.mirtv.ru/media/flash/player.swf?videoUrl=rtmp://mirtv.cdnvideo.ru/mirtv-live/mirtv600.sdp&amp;live=true&amp;emb=true" type="application/x-shockwave-flash" wmode="opaque" allowfullscreen="true" width="100%" height="100%"></object>'
WHERE id=24

UPDATE tv SET
url='<embed width="100%" height="100%" allowfullscreen="true" allowscriptaccess="always" type="application/x-shockwave-flash" src="http://fpdownload.adobe.com/strobe/FlashMediaPlayback_101.swf?src=http://93.115.83.34:5411/tntstream/&amp;streamType=live&amp;autoPlay=true">'
WHERE id=25

DELETE tv_language WHERE tv_id=26
DELETE tv WHERE id=26

DELETE tv_language WHERE tv_id=27
DELETE tv WHERE id=27

DELETE tv_language WHERE tv_id=28
DELETE tv WHERE id=28

UPDATE tv SET
url='<object type="application/x-shockwave-flash" data="http://pics.smotri.com/broadcast_play.swf?e60755baf4dd869cc38b4d07d7341b63" width="100%" height="100%" id="flash_live" style="visibility: visible;"><param name="allowfullscreen" value="true"><param name="allowscriptaccess" value="always"><param name="wmode" value="opaque"><param name="allowFullScreenInteractive" value="true"><param name="flashvars" value="file=vaaO1OzO1LvxmO1OEOYOmOCLmLzLvLYLrLtLCaDOzamatLEOtLraExvxYOaO4LzODODO0OvOvanOmeDOYOYOmOmOnOzO&amp;xmldatasource=http://pics.smotri.com/skin_ng.xml"></object>'
WHERE id=30

DELETE tv_language WHERE tv_id=32
DELETE tv WHERE id=32

UPDATE tv SET
url='<iframe src="http://tbn-tv.ru/veshanie.html" name="iframe" noresize="" frameborder="0" width="100%" height="100%" scrolling="no"></iframe>'
WHERE id=33
