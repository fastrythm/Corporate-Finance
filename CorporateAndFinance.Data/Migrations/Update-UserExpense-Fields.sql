﻿IF object_id(N'[dbo].[FK_dbo.UserExpenses_dbo.Expenses_ExpenseID]', N'F') IS NOT NULL
    ALTER TABLE [dbo].[UserExpenses] DROP CONSTRAINT [FK_dbo.UserExpenses_dbo.Expenses_ExpenseID]
IF EXISTS (SELECT name FROM sys.indexes WHERE name = N'IX_ExpenseID' AND object_id = object_id(N'[dbo].[UserExpenses]', N'U'))
    DROP INDEX [IX_ExpenseID] ON [dbo].[UserExpenses]
ALTER TABLE [dbo].[UserExpenses] ADD [Monthly_Salary] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Monthly_Salary2] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [EOBI_Employer] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [PF_Employer] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Mobile_Allowance] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Bonus] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Meal_Reimbursement] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Transportation] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Leave_Encashment] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Incentive_PSM] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Health_Insurance] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Medical_OPD] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Billable_Salary_PKR] [decimal](18, 6) NOT NULL DEFAULT 0
ALTER TABLE [dbo].[UserExpenses] ADD [Billable_Salary_USD] [decimal](18, 6) NOT NULL DEFAULT 0
DECLARE @var0 nvarchar(128)
SELECT @var0 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserExpenses')
AND col_name(parent_object_id, parent_column_id) = 'ExpenseID';
IF @var0 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserExpenses] DROP CONSTRAINT [' + @var0 + ']')
ALTER TABLE [dbo].[UserExpenses] DROP COLUMN [ExpenseID]
DECLARE @var1 nvarchar(128)
SELECT @var1 = name
FROM sys.default_constraints
WHERE parent_object_id = object_id(N'dbo.UserExpenses')
AND col_name(parent_object_id, parent_column_id) = 'Amount';
IF @var1 IS NOT NULL
    EXECUTE('ALTER TABLE [dbo].[UserExpenses] DROP CONSTRAINT [' + @var1 + ']')
ALTER TABLE [dbo].[UserExpenses] DROP COLUMN [Amount]
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201706050956549_Update-UserExpense-Fields', N'CorporateAndFinance.Data.Migrations.Configuration',  0x1F8B0800000000000400ED7DDB6E1CBB72E87B80FC83A0A72458D1585A590B7B1B760259976D6159F640232F1CEC97416B86921AEEE99EDDDD635B08CE979D877C527EE1B0EFBC14EFECCB4882014B6A9245B258AC2A16AB8AFFFBFFFEE7DD7FFDDC4407DF519A8549FCFEF0F8E8CDE1018A57C93A8C1FDE1FEEF2FB7FFFCBE17FFDE73FFFD3BB8BF5E6E7C19F4DBD5F8B7AB8659CBD3F7CCCF3EDDBD92C5B3DA24D901D6DC2559A64C97D7EB44A36B3609DCC4EDEBCF9EBECF878863088430CEBE0E0DDCD2ECEC30D2AFFC07F9E25F10A6DF35D105D276B1465F5775CB228A11E7C0E3628DB062BF4FEF02C49B7491AE4E8345E5F8671805B1E9D07797078701A85011ECF0245F78707411C277990E3D1BEFD9AA1459E26F1C3628B3F04D1EDD316E17AF74194A17A166FBBEABA137A73524C68D6356C40AD76599E6C0C011EFF5A6368C636B7C2F3618B418CC30B8CEBFCA9987589C7F787A7EB758AB2ECF080EDEBED599416F5602CE36FE8A85CA0A31AC22F07A27ABFB4B48249AAF887ABEEA27C97A2F731DAE56910FD7230DFDD45E1EA0FF4749B7C43F1FB781745E4B8F1C87119F5017F9AA7C916A5F9D30DBAA76773757E7830A39BCFD8F66D6BBE6935EDAB38FFFD3F0E0F3EE3A10477116A898440D122C7D3FB1B8A5131EBF53CC87394E235BE5AA312CDDC20982EF1B8518A7718EA3AFDDB2E5C037DCAE19CE175508F5B0EA39EFE710305EF12BCED0F0FAE839F9F50FC903F628670F297C383CBF0275A375F6AC85FE3107309DC284F77C8B8E7EBE42E8C90A4DFDFDE28BAD5EAE65310AFA330EEBFA3CBE067EF7DFC3DDC168B2EE9E7C44B3F57D9398A10A6EDA6A70F4912A120365EE44592E65FD2354A092AFDF5C482D2B1B0489F5CF05BD1A862B4787F3B118A4E27672587E8B98F14159CE94BDC7484A523BAC5D2D618F39F822CC7FC3EBC0F3B62D004F66ED6491DA92CFA10C4DF3EA4586E3C3A88A30EC8242452371C1BA144B71E4A2E15BDBACA936AD4C5EF121ABF2E58A53391575D29F8A197ED347F4C62F479B7B9EBF8586F3B3789F36095CF319D2671DF9D5D6C8230EABB1347B9A8D345ABCC3AEB302AB178BACAC3EFC8552AEE1F7B7664CC9361C9B6CC784836AC609E5E76CCB3A2640CEC73F03D7C28B12F94A408F3871B149595B2C7705B59094AF25CD2B52ED364739344756BAA70B94876E9AA405B22AA711BA40F28D71FE159B2D906F15301433242BA163342B2101E2155031AA13637202039300502CA247803311E1B16C1341F8A53D4DD3A9A11BCA87D060AAB4296AF56C5616F186DABEEACE8E495E37AE4B83EF858C3A5C47CACE174869C161C5C5D261C1F54CE0D11AC6439CAA2F13CC9C2729CB221B378211A71C317D6E5A486B2818398BBC58C22C3871CD399D1ED949323ABEBCE8F6AE3434EBACBC829C94707D9E828A4144A714F36EAC56392E643E8E3359268A96727406B500A99E6C704738EB2551A6EAB3B2A31868E4F5E05A8C7038195AC123040F5C94063A8C58F282CF892D680A9EAC26113B5548327AB9A4F21CE30D30CE27C1E052BB4C19AB76A125003681A7C3DC94480CAA6533947DB20CDC533E8CA97AD74EAC6CD9772FA0D50C554BBF99AA1F42C48D7173FB728CE14F4C257E6D1CCD611A298AB688ADE1240092CD41977575330E8A6827CC46D2D1FDA48B751DCF5920ED69434946E540EBA0A0D64BF4EF39FD043109DEE72ACB9E03E3DA94DD3D102CE891B5A6BC15DAD5174833641FA4D766DE067CCC5B5F24EDA8D9E0A6938CB7A7A32D70E3FF36B3A2AF8154BBD5A2D4F861AE289F9104F378585A82539B40A37417478304FF16FB5C31CDE438B5550ACCBEFCACBB0304298313CFADF933EAC1EE68A99C20202E97096F28BD38F9C2418076D22328C1B979D1403C10C25C7FE44F13A49BB41B85A847DB8B9F991ADBE8EA0DEDCAB8A3DEA45225EC46B188E82AD16F5E4868693DFF41CD28C17B421AF1B72DC4AF66C4737BDF651ED977EA731759B07CB334069C556521EC5B51A70324CAF959338E3A76B2DCB58509310649008309562E662C49F0883D4446B063531A1F302ECA7522E426C6C96C418AD575C0FD07B2595CD6F00354D923D3144D61E65C6464DCC69E0AC4EB7F8CCB02ABF1675E4ABA4ACCC2D95BA85A7938A9703CA24D8B91B23F7C6093B40DC3D99454CC1659866AABBBD9EAE150BC63788650E4BC0B5D48BE698B53C555087F5229E52B8D0E48E787B246ECF93D54E767F06C80FA2092879819AC07589B4BAD5358FEA868AA9B424B97E3711712D4E2A49AA9AAA0F80BCEC4B0D922C8558103B8AD56665BD88D706D8C4C46C332C37714B4219EE12CB9BB077B7FA35081847CA37BD2F9EB21C6D0691F79EC22D5FA2F0EA576649CE8B321167C52A1951E2C0271948936092CC986C3824006248C352D1B53367F361F4BF41AB106D0772B5D77312F0C3063F8451E4C7A7DF3478C1F0B2D8982F2F70CBAC6454EECC99F0751E055943B95FCC83A782AB9EA35C714EF5D25BCD543EA260DDF7963A8B42C4EB59B6FAD9AB4AA0A512389FFCD83393C621D1F4E82A1DB8D4AB5258497860153B603A292F1EB49649A92B2E7ACABEDD7CE1218F22508A8E7D2933165D5FA3A2EBDB307733530E190BF172B8BA9155CF92458A38BB9EEBB9368B642EB05C52B8D19026C130AF785544C928AF64AA564FD69D97707D74B1D946C9131A28F58DFB0D8FE639347C8803C539D44F789C37F6DA05DE70BA80DBDD9C9F7996BD60BDF53E4C37EE939D0759F62349D71F834CE627ED67E80BB4DA1581128B3CD86C7BEF4D2F9194F7BEBC2DCDED8FE43258610DF3222E5A39C3FB94ACBE25BBBCF63EFD9AAF4C1D505B005E8673BA5AA12CBBC4C48CD667A425C7CE8C5EC854059FB65C6AF1E1350AC20DACDCB07E2F4D55B12F4D5583536B04D54C0FAB9F92875010FFCFF6D054150FB5AAA11C6A5DCD74A80530BD91D635C5032D2B28C759D5B23DFEEB0D95A82D1E6E5B4939E4AEA6FFB8CAAE461514C9A9E344196CA8202BD884AC7602581BB76413398289B05A1D2C13D56D50AD3C013586A8FA278470A00A8877A89E0DFAE728DD8459264C1842D7016984290687CBD6E9C7F3C2B37FA38A643C3B623486A4725B159C5F76FEBC6E13949F66DBCF283F6A5A1F55702F530C13EB7ADF8E38B0BF1C6837EE4EA727BAA7D35F8FEFEE7FFDCB6FBF07EB5F7FFF0FF4EB6FC39F54452A8567539EEC64EC47CF2C574B61EFF3D8D39F41B4F3DD9515F197BA847FE22FC14E9FF81BE2B2B1698F61AE29D18ABF7D0FE50EB19ECE5E7547181B5320D54230F8A7D402EAF409B518254FA85E69BAE962689A1E662F995DDD39673D23A04CC21E4D8CC7FA0E8F0D3EDE8F6BBCE71932EDEB458AA95F78E9A47D30CC53244AF6006733728E03333AF583776F9059C08AB59167796BCED601990463A3EF144CF99AE44662BCD4E8C76FBCBC02A391F451F9CC8EDE555483433FEE12C370623F7261AFF9A771223D963B4972EDF562CA836D91700DA9210F30555AEB8A7EFCD3A7E69BEEE8973E924F7AD75F8F59E2F42586968BAD171FF8EB24CE1F23BC438328E81E18F3EEB94D7773D25B3F175F3E5C2D6BB78DB4B75EE697FDF75179832C4FA328F95125ACECA9A30F49DC25E7F33F0D1444CB1B146EEE766956E72DEBA9ABD29F1FB3C0E6CDD09EBAF98482EF687911AF82ECB1D7F95CE1558F0B8D65395F5CF7D6CB47BC40F9E3F22ACE7669AF74768DD6216EBBFC323FEF8F96C3AA7ACD6796F33F6E06EBEBEBA2BF7979CEFDFDE1E979EBAE8ADB6A6BCD103A644B1448DDE15217CD7E2FA56523862EAFAD95D9EEEAD8519FED004D42A575BEE47C1ECA6BB72AC6CAAB9DE1CBD09B01A27391C783158D036FC0F879FEAB8136096A07C6E5F81C180966A83D61F41AD90071C6BEA362755ECEE8FFC52481355CFDC492EBFE23E276FD6C4102E0D476213134C78DC840DAC3BD48CCC0CBB61C3BFEBB8ECB1EA5EF1BB442A1BFD407D34A133064AA85334C7D0F49AA7A6EAA270F88DB44B6BFF4468EF90E4A11F5D6874E403CB17BE8F71D5E53EEC01BCE0ACB7E0EECE44C4C41F956365C1E31D45439C0870F2DB58EB5CBFD4CD17C221AC5DAF23D209D2464C3DD6FF7C44547E1DD7E1D71BA6D3D06BFD3DE50EE779E53BAEF74B8EB1CE19E53E9D2E1C3CB629F3C39AC76A036A9CF519E3F9D9531BCD6C4DEC29804B9B7A3B12178AAF160A7CFC7002FF17DF3CCB92235C990AFC1F9E96BC247E2FAEF699D0CBD9D327CF8174FE1A06274C7731B64DF9A2C724E773C1DA049F0357A48B69E4B348421EF7F8A9E5D8D6BC541CC0351DF261E80A81323FA4A4FE2498F50BD6FA93B5C530EF218C40FE8B95FE637142EBC2A2C0A97F40EE41256417540474FB0A2B3AB67357E679E39296EE9C22787E4909E12D2F56283EE8931287437E593847A219E61F9C2F244CCBEE73B3FFEB02FC4438A6397BE792BE4902165C256BCB5E101FE037CF723B8775F92052A4C4FBEB340153406D334B9BCCBBA5A47CD7C2947C74015130A3ECDB2641596A36ADC488BCB8BE2BF0F85FBED23CA183AB888D70755D75CCD6E7015628B0956D731D798F6C2220D0A1E025E540E7332A0AD564403ADCA59D0FFC681AE2FBAF2302832E26598F4C338E729398C57E136885453631A829BA0B9FB9CB1BDCCDA6ED89273B42D5E068B731516DCFA6FBB61B6A80A47EF66049DC8C9877970BC188B9882A0CA1011B5415DFA740482064889BA35EC85966493D4594E61B0BB1145C9F0E13C8AE1E98A78C85E97BA8826FDD018D98198D2BA5AC3D01B30EDD1A80EC0D07ED09EF4D5213179E83D9B479248F776810919EA3D7504F5D33DDED6132DEAA0408F06C44F9E1A92A40EB67C0C6900CA6C923C028F5F88A845DC04A248A6B69A4CB43A929224DBC79BA3239EFAADA8513DA2412951BD127B46857C9A7E156D4872F6F33468C213C51D00B4674CE44ED4279CB3CE6A376D3D919E103DEE6319562EF3393B35A4A52477A747992C7901BDEB85ADD4BF3C164E7E2C692CC4D39E704176FC043F9F47C10AC99545ADD6107DAA284746A57A9D4AA5755BAF278A35C28B0EA5B0001D09D80885FEC637003D0BF2D38B684995ACBE231EEEBD1C0D7D4FD90F40A440666429893AE0A64E88AF3B66363B7E5FB861D2EB0B705327CEED0B37550A7EDD2133F9F8FBC20C9DD05F8098EA16A72FBC34EA933E6EDA16FDE3A7EB4AA22FFBC30D975A50AAD783790699E1C1D63CE579157CB8C001C9063800929889862ACB68D60D964C25A18F06492A341B6BA995D0178F6138D3A518C7FB60B214A41491D1BE2CBF08BDB92C8D41B2D4763A54EBD10A241E8ACEE2CA32751A1FC3C568F7319481498DCC06A3430A606A18AFC406BE88E226359DA90E1893CE5A8359D3ED880DC0BBAEC56732E791E23F0DD6A6FBC08F364DC8284FF379202D76E78FF6F4463528E7D35B953DE2806C7E20196F12260BA2391F99C2CA8CF989320D8DC5F804E31988E909D0BD770C4FDFC66DF048555F07C83EACDE56EE220636585923EF2E235333B4EA4C7EB8D3970E9EF6E11C463DC9A073332A6A60407F8A9D2AECC1F966D4162FED8B175A43E61EB6F0B32369D8225CE89A959CB62038CDE1F71E88913DDA74E2B4720A5A50A78CD37320D5203B8D747660575DCEC73E29518908036AF0E015AD8B332FA31A8F4EA98C4486F403A61AEA8D5AE16448606F5492C4216916C2C824C81642DEBE502E3B17459088CABFDF2A48647CCF7EE1DC7496D1579488AD43FFA8F4230CE5935935E4717DB4A25605389BF90BCA63B279F84DEA89DEBC056513D6B55EC091D1C63614196EDC873200C5018176225A9045DDF18E02909380E2042689D9EBC113A10AFCC3F8C38B16A3B4952275E07E1DB55FD62A5F1DE4A256F100EAC0D5AC0E7C64275674B0407963B75AAF539495CF607541878D91A92A039043C3A0021079306400A00624110865634AC4F1302811A3052A04A7D31E2BF58643063F09819171464AB080910C020CD8A214A0799B250F97B7026A8F573E4C03405D348F0C60172DA300CC997B78A89C694513A40C961610705BB2B65F051CCEB50E020AF8DF1980AD5DEAE4606BD73503B095379A1C6AC56D75D643B2A729739102567787054122AF083506A5A039137AEB6E4E44D0C83B2A7D5E4A24F897F1D4CEC8A10F9A4EE72B834E9D48951DAC05BC76ADC6A464417417A34D56084221323B6A2C2A9539035C5452B9D5042803A5BD3F557B13DC9784D6430B7D3A89C101518FD00024990EF8132798EBA09D4CAB6A70FAAD4E7603064C376E369D063D5F0D5C8011F9003AD491FB829B2CE06C4FCC86E09112C448C3F539684F20A6DD714329564A0C0963D0653383A2D05DB105059EF338A326E70173D29868107BFA51D4CC6CB5E2A8A919130AA2148F5A91D3206462AECEC8647441328C1740A4A4B678AAE246100279FD558245096829067D628EBF6095204E711B0B4E4E7C210BA04D0F5FE21B58839570DBBAC0F14CBA71152E28A2CD25F63F71DBB4622713022E30476714EAC5FA01B8B40812A4266F16266882058B6EA4BB9BC48333BE05B168008275A2D6A8A92AE2D698AD28479B22368D80051DA0BD63A93942ABB104C5AF4967C644B039618989521360A9998C772CD5A70E3592804036E9BCE850362714D1E16A020CD513F18E2042AAA991D455D69F5DDBC60FB23A701279EA4DF920A3D6443A8730B28DD70BA0D83676F85A67040E9A00193E100184AE01A85005B851C39784B81113A00C6712644882DA740E5F96B401BDE90C53882A548B5B5949B016B3BEDA2ABC243C4B0FE38E58A25E9296A3491866249C141468E48A2828B4A887CDA5887BD1E4C95A94A5192BE3CC9D872631EEF16698BCA4311C1C1988A23818EC503674056589E23606A02AAD93A0614482940EB4CE84E6E435DA91501A712035261A1D00B562141C0C8AA31EF384DEF212FC19D87294BEF5D6781BCD920379D1AB9005BBDB8BE7C539DCBBA189F3B1D7556FED112479605C882B4DEF71689E6AFF71CD1B0D33E0F2EB1262CE7DA195BE7BD5C6ACD8DF5967FEA0C7B347FC823ECE307C7AFECE58E69D6C4597989A5776625F5CF34BCC812FEA9ABB64FEED075895533B9772CA97D4BD94E151F595B7429D933A940210DB3939A30B7A56804794CA27929A90C42B12B04781B628153CCFF6ADE6CD83D6F5B12D7B375BAC1ED126A83FBC9BE12A2BB4CD7741543E70943505D7C1761BC60F59D7B2FE72B0D862BD0793F9BF2F0E0F7E6EA2387B7FF898E7DBB7B3595682CE8E36ED5B1DAB64330BD6C9ECE4CD9BBFCE8E8F679B0AC66C45E19B75D46C7BCA933478404C69E5937219A6597E1EE4C15D5078919CAD37403581A3A7C027A3E996F5E5E457B2F1D2685A14BF33CEA5C4AB50C5285BF74F065887D74B3CD5429B2C678D084210B6C46D8BE71783947DB9B16A50381C9F25D16E13739F59E214C3A21E4B27A15105FAF09A67AC4950F093D81A333C06276802E83AB90B0BCF1A124CF34D1FCAA7205E4798E66838DD577D4897C5F32B2490F2837EFBBF87DBCA318B84D17ED487433C064542223EEBC322DEF62561119F4DC86717E7E9134B3FF5478331E5E50B55D478AA4F06630973762021FF069A1442F7361505A6FB6C4283E4EB54341D92253CC477338603B1FC6EC6313C460EB11C548BBF2ADCCF2C582CE11A6FCE65658D4538EFDAB02C8D2ED15FC5263489856508A5ECB9F89D81447C3785C6B315F2BB3EB4F96312A3CFBBCD1DCB0FA802138E80E5FD2A9F632D2A61F7115DA40FF362533A8092B0EA4FC371F25698394AB8EE65559A91375F5FB99536B7F2C8A62C1994196B7267243C0B31651EAFE4E785FC14660E0B2A2403B8CC8951DA5ACCAAA9F06D9A554B23BB953005F04611BEDE9582D3D5AAD07021A1C91419C3AC9EA705205605AFFB7C947D0EDF50D86F72FB0D6EBCB9BD6C4477B1B378C4A74B1E0CF1D998C540DB8F293286C96F3FAA401F1EF5C635098F2A78DDCEE3886D79B88CFDBE26029DAD77B80C86827ABBA6825D4F571847A87F420F4174BACB1FCB27D2799600958FB7EFCE39B3D4B9A155EA6F28466910DDA04D907E638E8F6C9999BD6CC7406BBE991893CB8E19B36DF7D51C527139C25BA6E93263A82720BC131B4892F19D988FEF7453A8668CBE567F33B04C84119A07F923639E68BF4E887B6AF90759F14F3E9F830D07D58022B15BB18D797E0756D15F69E84125B2039D07976433F07195E393D7FBD35A7CDE7F148FDAF37CBDFB6A60978CD73CA0F6A30177C2F579D5B3FB6A647DAD69E7861B185B664C131048A2C0741FF0E0C8EFAF1AAD164FD6F17BB560C85C221C736EAC06310E9B84540073D12F1E9DEDB8FC334B9857BE6E2D6375C7BB96E3A4DCD8E834BE88B46B079B5FD852132D38CD000B11F1D98CD4809365600EE96F4532406696CDB761EF2FFD78C64C53237B6533CA0C244EECA64D16E7C276C440D4CCA2692B6643648D31A5B09F4354331B9E09D125E610174F598E3662B864F9382E61AF8CC11363D00827B2E00A0C540B96A08420D3BC89869012CE149B69F545631150C3FDEB6CF6BD41AB106D412D89291ACFB8FD218C22DEECD07D1DDA107A952D82086537280A00F64197E943254261F8C97285C62665D0966CE480173C15DBACC97248B9E0D145060A67B5853EA28041235560C087A31092DEEDD771CFCDAFF2C6ABBCF12C681C248CB968F1C3FE3D998870BF804341FBD50C1278DE26BE9B41BB4645ABDB30678F945CE1EBD972CFB7B4AF7829262FB7F9AE56421092034B0766D73D93342C5D6CB651F28440CF7BB66C6823115627C3873800F5CCAE609CED4CBFB14B0F4EF6FAAE6C25DC8D736503ACC0DC87E9869D265B66A21966D98F245D7F2CF369D38A21596270CE47AB5DE156B3C883CD9639EBD345E3059010CD0418856B189C087E249758E74FD28B38B88B58E87CA9017F4856DF925D5E5F017FCD570CA3E08B2D60036366CB8CFC8E51965D621245EB33E010C7179BA9513CB7ECBE4E464C022930BDC84CFED90973A9A901A31FB9592ABC6B4009363C39E221035A70F7D910D69F41B48380D5DF274953C29CA68E3455BD39E246530218FD5245D929FEF63DE46CCE4C9181DCA8DBFC81982860AA6092F4214AE7EA481EE523156ED40183109AC4706D96389A6F7D319E318D257E83438834A9B63613D32011A2116839B171FAF3E55FF3DCDD10BDA66178C9460E3249A7978D284E50AAB10F658D873BD4F26AB7A981C267C456370B5E1364CB468989F2C86C5EF456A492177B138A6E57D556D7D4922B6AEBEB6977A1D883F1AB9A0CE0C54E16981820E3FC317A5A2E8A32460F67CB6CA19EC8C01A45055D7CF970B5ACADADAC0D962E3238865C0A205205A626DDE56914253FAA275479E32E596A70FD9FC46C0859FDC960742888963728DCDCEDD2AC7E87951A1F506E78758FF9420ED8A2D93203AE8A82EF687911AF82EC911F315F6A2091F00AC485F059CE17D78C58A28BF4617EC418CC1F975771B64BF9F5E74B4DD66E1DE2EFCB2FF37376D18802337792C2FE58EFC4E5FC8F1BDEB384AB600FFFEB82CDE7005518459FA884FD07361D59F7F9559FD0D627E644D67A6F2A4507D452AB9001E8D11EEBAC4274E36661D125932101CDBCD76E1977DAE77DCD89410B8AE2E041369664E221AB181F6EBC66F971F78F34F3181C9FF61499C1DDC88F7CFFD98902A580340884682FA115A6D6B8A4480C86A74AAE707CB7D1DAA79387491518391AA350E8684C160DEDCEEBDB5DF90CAFE0439242497AA81283354E2414C9159A8F5498AC19AC60459B502210A0F825FBDE89778EE754DA1EB57E724E2C40B66C42E272ED2FA1D2DA2E8712D44C2C8E7CC47AB99BDA01F962C8C87AB84BF24EC67B71E0F46BBCB6375C1B1BAD05066B2B63F51428DA9F31C6666F8C447D7394E74F6741E62D037B0BD08204256DC5C685BA096F5B200A0C58D1638017E9BE49810A442B4015C6D350F7ED2CE22B6CCF9FC6E8C97964127AE7883653F953539636D30EA8A5CD54064046105D3B8834E85233522BDA8A609A412BD28741C44B7E37399842B0BAAF26871C3F51AA1EE5B197D49598F3C70F885352DBAFAFCCC288597866130E0CC29C35F8D9C0807631BE3EE1AC46CFD3B0CCA3CB7A44375F470AE2DA011E27EDC7D77BD6D19CD17B704477744237734077BB61D53FF7F6BD4CF44392F45A3161B4CB3A60C92286B76D6A1C73543C9EC99E2540D8CDC39C3C06B5D68F050AAD6781CB7630D6E3AC1FFCB41CA7F1B8CE92785DDEE51E5C659F7751F4FEF03E8832F6E90FE5ECD927479D89A98E54B221A6A6A971B091C62255B0A74F4CF538CD88C924DE652F89AA7E2DD782A6EA96A6114A1A2B55429E3E4155C334A327B328AC174A935DB21B1BBA245AEBE6AED158EC16EAF4E9B21BAA39AF33CDD3B317F44584CB95C8D1222BBE9141541FB04A2C3C47ADC617F170C332236FFB204577CA198A1DF169DC6DD81200C5342BBBC6DEE77A7164577DF1287E9C66DBC1471AFAE95220B921A92CA306BE8B4063C364A1C0328A601B52993CA66F69CB350CC66B2719ED93A3BAD39A1239CE54479A7596557632536352D3CCC3A18007EBC8CA28801E581A30C0E77E1650A0504D818D6DAD7C6D3C8C51CA56698D77F597F6EFACF9509056F080AE93358AB2AEDD62F588364189956C1BAC50E52D5626922BA8F42EC85055E5F0605EE71C797F5825E4AEC878F18FA8CA99DA55B80EE2F01E65F96DF20DC5EF0F4FDE1C9F1C1E9C4661901529B9A2FBC3839F9B28CEDEAE76599E6C82384EAA80B7F7878F79BE7D3B9B65658FD9D1265CA54996DCE747AB64330BD6C90CC3FA75767C3C43EBCD8C6D5E83D582F2E6AF0D942C5B5357B78465B891C1F583E8F49AFD81385A6868E406DDD34D21A6C3B67FC74AFEAE693196F78777E1435860B9E411D52B7B395ACF833C4769DCD1D8E141418645C8584B8A336947946768D5D52E0EFFB1436109F13E2C747843988DC7213D724320ED43F41594F87B90AE1E033C98EBE0E727143FE48FEF0F8F4FFE620CB7491E588185A0FEF686049AA77C122616E6A7205E477863FA857A19FCF40BF0EFE1B6742795003D31064A5C9735EB6DBED884EB5A05C48662CA6C6E4518B68462CC91565CE423CF30CFCA9DEA176477AB57C15DE33FF3B0B8EC3144237DA167048CBC249232D5EE9D721BBE2A7BE55CCD5AE9D6FD72D726D082EE85155D6FAFE235FAF9FEF0BFCB566F0FAEFECFB26AF8CB41B929DE1EBC39F8BFE67D9773ACEEFB6852FB974DF0F35F4D09AC0247F20F4F944B25D5F4BB270A7D6995CFB19299C49E61D70955BDC22418BE27881642548FE7374E52F62C7FFF18962DABB26552FDB32788393852DCCB230D22A6CE8642A421796A42619AF74B2F8415457454D0906D2D1437F136A66895A810A6C79CCAD1BF17F9D70411941E76AFBBDCC72E77D8E10EBBDBE3D95CC9F26DCED4440A3DCF0A5C397D7A73586EB41A16B419ACF461CA015636EBC2FCF5BABBB47657F1230ACBAC4FF6FBAC03E2B0E368202F47AA7E420F4174BACB1F4B6F6A536631F2BE39272C35D6E45E2D6CD40694F81D621316A2CD26358DB8E5608F7D8FB681DB04E9A8659001D4939E467BE277B44DF4604D5668156E82A8B8EDC0BF65E5B5C531DE03C535132EFEDDF8C81F4608739047B75D66C0679BABFD7914AC109FD35797D37260EC782D08A65F6E0BF938189F2178204E5CD7CB85C994C4880F0DC6CBC54211ACE8452CD42FDCC8E068F128DC8E5441C13B98DF2CD6BEA1C41B729006CCCA84C47AEDA3DA5AFD4E63AF7463B97F9B1EC3563B76A9B9B59A71FA66D5FA82DC709338F3484F7CDF279BDCFBC322BCFEA5C30CAB1B69AC51DD965E9E9E752A3755CA4D83EA659FD086171BA5847893D2B3CDA97BA2D2EFF914F32FC20F21E641BA5D15FA318CF5E1C432199D6D12DCC862FB9F27AB9DFB89AA81E2C60E48287D5BAFA62655BD9CA61A04F6C2BB1AE09527651F5CCC8B43D50BDCCD52E77EBDADACF46B57EF630044FF9A7613AC666C0CE91A3B6D5B2FC7F41B7C4244DB7EAE53752DD716DBB578E7C1F09A56EF76D9DA78A9C92016B8657683A2C0914B7029DF7A3190FBB638D709AEEB0C609E81D79BFF230A801390DBB56AE9B1EFCF443EA80A30984FF2240497D006B7F46AEE559B7D1D0EC870FCB899307591A2FB67A8C283EE830B16708D25A336E46B5440AE338C4DCEDBE839B30428198085C98C073380F58CE9D486474021752ADE00A1C8F150B77FA6AEFAF1BE7E9CEF6913953F153C7C88034805B772E2F2C215E8972A8D8F4F647317A5078C48B0C24A0909EB62F761BA7143CD3CC8B21F49BAFE5824DDF631B4055AED0ACFA4451E6CB65E204AC24F5CE17941E1ED8FE4121F8C92F4222E5A39C1FA94ACBE25BBBCBECBFE9AAF5CAFB35B80CE433B5DAD50965D62C243EB33F2D06A63B82A2409C4303516545B7029320A0E21BAFAD36539E968B5154AB4408AAC3DB43F8368E707DC3E2B4D8A0484FA472B1BFA8309C45DC929E631AF7308F8E1C435303CA1578A91A612D1231851960D8FB4D574E199B67A22D9091204242E98EC23162364600C40AFC22C71066623FBB01BA6F97E198F5EDD407BBA0E9DA8F5673FBCA4BA53AECD7EA68FD8A6DB5976401F3CA4FAF88D790A12ADF03AF3D42D1D5E0CADCD8305B14D88974D62F71BC94F471F0647FF85417D17BA5E8CD91F4B16AED16DBDDB02F5C2152AEC7BF1A9B84EE2FC317A5A2E8A4C7AED51CABB3301DDCD496FFD5C7CF970B5AC4DDF696FBDCC2FFBEFA332B12F4FA328F95106CDF6D5D18724EE8216FD4F0305D1F206859BBB5D9A5531697D7555BA996CB134A5AE10BC77F30905DFD1F2225E05D963AFF3B9C2AB1E175276395F5CF7D6CB47BC40F9E3F22ACE7669AF74768DD6F8201C2DBFCCCFFBA3E5B0AA5EF399E5FC8F9BC1FAFABAE86F5E1E93067C7812EB5BCF5E6B9AA3741366C5820C6FD07F112A52876043276D9B8C4CF3242BF3F33A66666AC038666822C1F4BBE2D2B450FA67260FB992BCA8A05EFD586DE888F04F7524250292233531905E0A4171AF4E3BD256CFAEC7B58F701FA06FD00A8516CEEE53701DEFD199FE0C53C2439282A99E9CEF7C6E13E94EB01BA961126E1DD00455D339675E762010B489BCE3DEBF1A4DCED81AAC81DC5B5B26E05A5BE6DC8202F686B7CCDBB0833E788CCFDBAA8E5A267DF272B0553BD8A907B3516B5CDE185FB04CF4CEC68278B5A9648EF2FCE9ACF025B5A093B6B10DA5508D7BD6B4F13AE65FEE9BE4ADCA788A1E73F059801E483BDF0F25D78BDEE4DB3F640A6A989145EE36C8BED521969657991D04DBDB4C1A42FFD6BAA23FEB60ECAAB1D321BBC89BE699F06E13CF000541BD9681201E241F98DB931B8EAE10881FD08B36C41754ECB2E15DB67AFF9BDC344ED2D5A86247864A1D93CE55A8E7951D96B98547B7479CEFFCB847BCF44B33D239788FC337A1239A43BC10F1A66373191CC4DF96DDCB11887D52F0225E1F1438AC1F9BA98752BC9878547DB8DE457958B88AE3EEF01CB909D100EAE7B55830CD671AD8BF71C06AB3581E0645246096A701DEA8FC6286F12ADC06113970A612B8E6F06B2705365B906CC939DA1699D9E29C9FA35B8F2D6086065518A0DEED942F3D9365A1188B78F51BB77772E5DA6FFA34403E4B03808248CA1315085E77975D8C39D282F0091E9B5E87A787EEB181A1A8827868010048963E2F0A113D3031593A69F2E12CF9D47E325A693392D2ABDB7D36A1182EBF210CB42BEE8966E034ABC2A5933F626F4039F2FC8E96FD0F403D4C16B9A53051AD03E5BC393A92110F9B4E8F04CA953D2BB29126129C3ECD2C99F18B994D9BDD095A5B1346333EADC089AA048B25CE4CD53B9DC8BB1E562EB159E107924A5C527C12245FF8AC588BFC4100CBDE07A01A76DC84406ADF7F11938FD38AEB2938DD9B38308112E5BD1094D5C2AA9F76B05477042F04390D63002A63A3E9CBB42162B262F38F912BCF9519E93E7C8218123650DA0B4D49F3AB09D6124CF2A04F428ACC38824EA1640A9320A03231C9D8045465791111505DFA1C0908486FB36F04544C6A6CFA298DF322F2A90A9F23F5F07712FB463C8DAE3F0601191DEDF697608C8E7D9321142247CD12C89C3A187D007669EAFBB3A21203BBF5B8D4D12512588283F67FB34024782121919F9FC95D8228958D4DA703318AC6042CCCC2E3B08E2A0E21B2E7F56BCB335C2459822163366162CB53753C3081D43F471428E390CB4802C5D4EA3B19AB4AF15F1FCC4441200EA4B7E77CC5864027C25BBA80FF31155522AF03CB5AC8A2E7C45D44A92C043D4E8AC1E8DF2BF5473653B8641A8174ACAE21C63DEE740E5506374ADE5CAAA6727F34865B95DD6DD1D86722D270A2E5E560402B7BE012333499D839C370EF9C8D4F2AE5EF61FF6E9AE359D646220E5FBD0E4710A53B3F90DB49491B261ED99A0EDE6D962B01D8AEBC4F9A3175BFF61004A0CCF4653B82F1E88808781E8394C8445702C85495E74B50C2945F13A62996987A8C311A2FBE64C82823534A1C75F59BE8D53608BC8A5897FBEE9661B6AC8E517D34D35788307D085C53D49BE72E1F2F2C58237180B0B1DE224A4D60D5F30014423DE4528C464C1B545827E43902798DEC81478A385C55B06863B8A3708FF60C402D17658FB80D26E718A5AD545BA3F265D3F3200FEE022E4553D56A81F2C68EB75EA728CB0E0F2EDAB0D8C616D7942C568F6813BC3F5CDF259800AAC0DAB690A31D1A3C19C2CAF54016429D90E51AFD087A10C35642A56423079C2A85FAA02AE87525EE46DA852E78325250D4115947D225594DD939605103BA076AC103002A2A86C05B7FB9FEF92A50E77C2DEDC94BE7AC9AAA413F5D609FA4BFAE92BCDFAE9EA27FCE16C675CED5807AE62A69762BE94FDE91B207EE4E82E7946C0D906366DBCF28AF64B8AA4BC0D99CEB14A823EFB676AC37E9BBF65396F65DD791F75DFB649BF45DA914D2AEAB2AF29E2B675E2D2A127260AA54484B9A9C98BC4BE77A220BA18EC8728D29C9F7A2721FB656769DBEC84B59B03BB282A847B28EBE6CEECC653219DDD552C8EAAEA2FE10281B8B6C145445C540A8BACAB1AC6199BE168AF1B59ABB8A09484A3CDA84D32588E47B20CAA03E88620DEA244FB42075921544D4491ED1753B957427EF489F5B2A38A58A4BC21C923874D05A369D12E780A847E8DC92BC398055AB9D18AC96732DE80345DB4E705498D153D1982698FE0598A93A4D0C7CD7448C5B2035C4963BBE2D78BC709E3499E3443D756146149F08E00F2F2404C979C40619D2441E2042F4537F309363CF07F5AC449ABFA035ABF53350847ABC397218159DCC5301204652BB2FA4088E222508D5F9C21E1DBC7382041B0A4F06703AE03C268408699601F99651788F79A40D9121A084A13CDC9B23452F881EC08E45F4BDBF890A10CE996F18CC8BED31E68813C4850398D28920A7262438D19793E1CA2488111ED34B4840A977A434E76A3552A0A8E8DE9142D90F38A4D4A5DE91522BB46A9C0081BEBDA38454D6398C5485DE11D2B07A2DA47495FB478C9164B315CD64CCA548220BE332FB9B36A00BCBCC48E69307420A81E9AB020FBD28F0BCCDAB6C2AB166D9AD35103627587155809DDBF0B9D51629607EB55028284C317F61FC586F84DF3F2A14B14F9A3C7000CA7040AB2585B0A13D02EA904600F54619BC05B9250E895DD89D3EB40E2B86F12EBD2069E8B38B344A436A14323AA9D89B85C6389708431124083138ECDB2263E8A33EE464AFC200EC8DEF6FFABDAB544A5772310634DDCF5D0DBFA2F6EC15190B4778E7E50D49949FB43E9EC4EED57DA10AB8CA6341C92EE6CC11C63B018B6E55342F1ACC6F5586B96268AEB6387F57811AA2F68DE5780079E5D63280EAA3827BF0378454FBA6C81909804B27307D95E327685F10DA1620BBC220168AE6B58DD64BB12D7B37AB6E24EB0FF8CF3C498307749DAC5194955FDFCD6E7671F1F249F5D739CAC2870EC43B0C33462BCA2BB2AD7315DF278D972633A2A60AF39CC8355EDF759007A7691EDEE3AD8D8B57086B9CF1C3E1C19F41B42B6EB53777687D157FD9E5DB5D8EA78C36771125550A274F59FFEF66DC98DF7D295FFFC97C4C010F332C1E8BF9127FD885D1BA1DF725F0588C0044E114503F9A54AC655E3C9EF4F0D442FA9CC49A806AF4B54EAFB768B38D30B0EC4BBC088A67BBCCC786C9EF137A08564FF8FBF7B07CB0520444BD1034DADF9D87C1431A6CB21A46D71EFF896978BDF9F99FFF1F245DA8558C600200 , N'6.1.3-40302')
