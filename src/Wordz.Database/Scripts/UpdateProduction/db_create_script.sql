IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'wordz')
	DROP DATABASE [wordz]
GO

CREATE DATABASE [wordz]  ON (NAME = N'wordz_Data', FILENAME = N'D:\Test\LearnWords\Src\Wordz\Wordz.DB\res\wordz.mdf' , SIZE = 67, FILEGROWTH = 10) LOG ON (NAME = N'wordz_Log', FILENAME = N'D:\Test\LearnWords\Src\Wordz\Wordz.DB\res\wordz_log.LDF' , SIZE = 240, FILEGROWTH = 10%)
 COLLATE Cyrillic_General_CI_AS
GO

exec sp_dboption N'wordz', N'autoclose', N'false'
GO

exec sp_dboption N'wordz', N'bulkcopy', N'false'
GO

exec sp_dboption N'wordz', N'trunc. log', N'false'
GO

exec sp_dboption N'wordz', N'torn page detection', N'true'
GO

exec sp_dboption N'wordz', N'read only', N'false'
GO

exec sp_dboption N'wordz', N'dbo use', N'false'
GO

exec sp_dboption N'wordz', N'single', N'false'
GO

exec sp_dboption N'wordz', N'autoshrink', N'false'
GO

exec sp_dboption N'wordz', N'ANSI null default', N'false'
GO

exec sp_dboption N'wordz', N'recursive triggers', N'false'
GO

exec sp_dboption N'wordz', N'ANSI nulls', N'false'
GO

exec sp_dboption N'wordz', N'concat null yields null', N'false'
GO

exec sp_dboption N'wordz', N'cursor close on commit', N'false'
GO

exec sp_dboption N'wordz', N'default to local cursor', N'false'
GO

exec sp_dboption N'wordz', N'quoted identifier', N'false'
GO

exec sp_dboption N'wordz', N'ANSI warnings', N'false'
GO

exec sp_dboption N'wordz', N'auto create statistics', N'true'
GO

exec sp_dboption N'wordz', N'auto update statistics', N'true'
GO

use [wordz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_account_verb_account]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[account_verb] DROP CONSTRAINT FK_account_verb_account
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_account_word_account]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[account_word] DROP CONSTRAINT FK_account_word_account
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_order_quiz_account]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_order_quiz] DROP CONSTRAINT FK_word_order_quiz_account
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_quiz_account]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_quiz] DROP CONSTRAINT FK_word_quiz_account
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article] DROP CONSTRAINT FK_article_category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_category_language_category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article_category_language] DROP CONSTRAINT FK_article_category_language_category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_domain_word_domain]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[domain_word] DROP CONSTRAINT FK_domain_word_domain
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_domain_word_language_domain]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[domain_word_language] DROP CONSTRAINT FK_domain_word_language_domain
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_film_category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film] DROP CONSTRAINT FK_film_film_category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_category_language_film_category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film_category_language] DROP CONSTRAINT FK_film_category_language_film_category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_film_player]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film] DROP CONSTRAINT FK_film_film_player
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_account_word_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[account_word] DROP CONSTRAINT FK_account_word_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article] DROP CONSTRAINT FK_article_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_language1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article] DROP CONSTRAINT FK_article_language1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_category_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article_category_language] DROP CONSTRAINT FK_article_category_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_domain_word_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[domain_word_language] DROP CONSTRAINT FK_domain_word_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film] DROP CONSTRAINT FK_film_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_category_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film_category_language] DROP CONSTRAINT FK_film_category_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_fm_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[fm] DROP CONSTRAINT FK_fm_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_fm_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[fm_language] DROP CONSTRAINT FK_fm_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tv_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tv] DROP CONSTRAINT FK_tv_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tv_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tv_language] DROP CONSTRAINT FK_tv_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_language_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_language] DROP CONSTRAINT FK_word_language_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_order_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_order] DROP CONSTRAINT FK_word_order_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_order_quiz_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_order_quiz] DROP CONSTRAINT FK_word_order_quiz_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_quiz_language]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_quiz] DROP CONSTRAINT FK_word_quiz_language
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_article_site]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[article] DROP CONSTRAINT FK_article_site
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_verb_verb_type]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[verb] DROP CONSTRAINT FK_verb_verb_type
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_account_word_word]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[account_word] DROP CONSTRAINT FK_account_word_word
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_domain_word_word]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[domain_word] DROP CONSTRAINT FK_domain_word_word
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_word_language_word]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[word_language] DROP CONSTRAINT FK_word_language_word
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_film_part_film]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[film_part] DROP CONSTRAINT FK_film_part_film
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_fm_language_fm]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[fm_language] DROP CONSTRAINT FK_fm_language_fm
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_tv_language_tv]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[tv_language] DROP CONSTRAINT FK_tv_language_tv
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_account_verb_verb]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[account_verb] DROP CONSTRAINT FK_account_verb_verb
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_verb_del_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_verb_del_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_verb_get_info]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_verb_get_info]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_verb_get_top]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_verb_get_top]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_verb_get_top_20]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_verb_get_top_20]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_verb_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_verb_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_clear_all]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_clear_all]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_part_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_part_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_verb_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_verb_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_del]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_del]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_get_count]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_get_count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_get_top]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_get_top]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_get_top_20]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_get_top_20]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_word_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_word_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_article_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_article_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_article_get_by_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_article_get_by_id]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_category_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_category_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_domain_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_domain_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_category_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_category_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_get_list_by_category]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_get_list_by_category]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_get_list_by_search]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_get_list_by_search]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_film_upd_status]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_film_upd_status]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_fm_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_fm_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_fm_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_fm_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_tv_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_tv_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_tv_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_tv_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_by_domain]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_by_domain]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_by_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_by_id]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_by_ordered]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_by_ordered]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_by_original]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_by_original]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_by_random]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_by_random]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_count]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_id_by_original]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_id_by_original]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_id_by_text]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_id_by_text]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_list_unsounded_short]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_list_unsounded_short]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_ins_use_exist]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_ins_use_exist]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_order_quiz_get_place]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_order_quiz_get_place]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_order_quiz_get_top]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_order_quiz_get_top]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_quiz_get_place]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_quiz_get_place]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_quiz_get_top]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_quiz_get_top]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_upd]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_upd]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_get_count]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_get_count]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_ins]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_ins]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_account_upd]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_account_upd]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_language_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_language_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_level_get_random]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_level_get_random]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_level_quiz_get_place]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_level_quiz_get_place]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_level_quiz_get_top]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_level_quiz_get_top]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_site_get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_site_get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_verb_type_get_list]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_verb_type_get_list]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_count_sounded]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_count_sounded]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_get_sound_by_id]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_get_sound_by_id]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[pr_word_order_get_random]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[pr_word_order_get_random]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[account_verb]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[account_verb]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[film_part]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[film_part]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fm_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[fm_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tv_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tv_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[account_word]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[account_word]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[article]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[article]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[article_category_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[article_category_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[domain_word]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[domain_word]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[domain_word_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[domain_word_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[film]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[film]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[film_category_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[film_category_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[fm]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[fm]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tv]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tv]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[verb]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[verb]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[word_language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[word_language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[word_order]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[word_order]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[word_order_quiz]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[word_order_quiz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[word_quiz]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[word_quiz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[account]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[account]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[category]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[domain]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[domain]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[film_category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[film_category]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[film_player]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[film_player]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[language]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[language]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[level]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[level]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[level_quiz]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[level_quiz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[site]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[site]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[verb_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[verb_type]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[word]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[word]
GO

if not exists (select * from master.dbo.syslogins where loginname = N'cra_user')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'wordz', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'cra_user', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'dol_user')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'db_dol', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'dol_user', null, @logindb, @loginlang
END
GO

if not exists (select * from master.dbo.syslogins where loginname = N'wordz_user')
BEGIN
	declare @logindb nvarchar(132), @loginlang nvarchar(132) select @logindb = N'master', @loginlang = N'us_english'
	if @logindb is null or not exists (select * from master.dbo.sysdatabases where name = @logindb)
		select @logindb = N'master'
	if @loginlang is null or (not exists (select * from master.dbo.syslanguages where name = @loginlang) and @loginlang <> N'us_english')
		select @loginlang = @@language
	exec sp_addlogin N'wordz_user', null, @logindb, @loginlang
END
GO

exec sp_addsrvrolemember N'BUILTIN\Администраторы', sysadmin
GO

if not exists (select * from dbo.sysusers where name = N'wordz_user' and uid < 16382)
	EXEC sp_grantdbaccess N'wordz_user', N'wordz_user'
GO

if not exists (select * from dbo.sysusers where name = N'wordz_users' and uid > 16399)
	EXEC sp_addrole N'wordz_users'
GO

exec sp_addrolemember N'wordz_users', N'wordz_user'
GO

CREATE TABLE [dbo].[account] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[nick] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[email] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NULL ,
	[password] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[category] (
	[id] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[domain] (
	[id] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[film_category] (
	[id] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[film_player] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[pattern] [nvarchar] (2000) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[language] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[level] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[sentence] [nvarchar] (150) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[answer1] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[answer2] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[answer3] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[correct] [tinyint] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[level_quiz] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[account_id] [int] NULL ,
	[nick] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NULL ,
	[result] [tinyint] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[site] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[url] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[verb_type] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[rule] [varchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[word] (
	[id] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[account_word] (
	[account_id] [int] NOT NULL ,
	[word_id] [int] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[article] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[title] [varchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[body] [text] COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[category_id] [int] NULL ,
	[site_id] [int] NULL ,
	[order] [int] NOT NULL ,
	[native_language_id] [int] NOT NULL ,
	[learn_language_id] [int] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[article_category_language] (
	[language_id] [int] NOT NULL ,
	[category_id] [int] NOT NULL ,
	[name] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[domain_word] (
	[domain_id] [int] NOT NULL ,
	[word_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[domain_word_language] (
	[language_id] [int] NOT NULL ,
	[domain_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[film] (
	[id] [int] NOT NULL ,
	[name] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[url] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[film_category_id] [int] NOT NULL ,
	[film_player_id] [int] NOT NULL ,
	[language_id] [int] NOT NULL ,
	[status] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[film_category_language] (
	[language_id] [int] NOT NULL ,
	[film_category_id] [int] NOT NULL ,
	[name] [nvarchar] (30) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[fm] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[image_url] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NULL ,
	[url] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[use_media_player] [bit] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tv] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[image_url] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NULL ,
	[url] [nvarchar] (2000) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[verb] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[form1] [varchar] (40) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[form2] [varchar] (40) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[form3] [varchar] (40) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[translation] [varchar] (256) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[popular] [bit] NOT NULL ,
	[verb_type_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[word_language] (
	[language_id] [int] NOT NULL ,
	[word_id] [int] NOT NULL ,
	[text] [nvarchar] (254) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[word_order] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[sentence] [nvarchar] (150) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[word_order_quiz] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[account_id] [int] NULL ,
	[nick] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NULL ,
	[result] [tinyint] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[word_quiz] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[account_id] [int] NULL ,
	[nick] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NULL ,
	[result] [tinyint] NOT NULL ,
	[language_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[account_verb] (
	[account_id] [int] NOT NULL ,
	[verb_id] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[film_part] (
	[film_id] [int] NOT NULL ,
	[number] [int] NOT NULL ,
	[url] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[fm_language] (
	[language_id] [int] NOT NULL ,
	[fm_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[description] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tv_language] (
	[language_id] [int] NOT NULL ,
	[tv_id] [int] NOT NULL ,
	[name] [nvarchar] (20) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[description] [nvarchar] (500) COLLATE Cyrillic_General_CI_AS NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[account] WITH NOCHECK ADD 
	CONSTRAINT [PK_account] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[category] WITH NOCHECK ADD 
	CONSTRAINT [PK_category] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[domain] WITH NOCHECK ADD 
	CONSTRAINT [PK_domain] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[film_category] WITH NOCHECK ADD 
	CONSTRAINT [PK_film_category] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[film_player] WITH NOCHECK ADD 
	CONSTRAINT [PK_film_player] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[language] WITH NOCHECK ADD 
	CONSTRAINT [PK_language] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[level] WITH NOCHECK ADD 
	CONSTRAINT [PK_level] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[level_quiz] WITH NOCHECK ADD 
	CONSTRAINT [PK_level_quiz] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[site] WITH NOCHECK ADD 
	CONSTRAINT [PK_site] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[verb_type] WITH NOCHECK ADD 
	CONSTRAINT [PK_verb_type] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[word] WITH NOCHECK ADD 
	CONSTRAINT [PK_word] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[account_word] WITH NOCHECK ADD 
	CONSTRAINT [PK_account_word] PRIMARY KEY  CLUSTERED 
	(
		[account_id],
		[word_id],
		[language_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[article] WITH NOCHECK ADD 
	CONSTRAINT [PK_article] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[article_category_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_article_category_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[category_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[domain_word] WITH NOCHECK ADD 
	CONSTRAINT [PK_domain_word] PRIMARY KEY  CLUSTERED 
	(
		[domain_id],
		[word_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[domain_word_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_domain_word_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[domain_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[film] WITH NOCHECK ADD 
	CONSTRAINT [PK_film] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[film_category_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_film_category_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[film_category_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[fm] WITH NOCHECK ADD 
	CONSTRAINT [PK_fm] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tv] WITH NOCHECK ADD 
	CONSTRAINT [PK_tv] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[verb] WITH NOCHECK ADD 
	CONSTRAINT [PK_verb] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[word_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_word_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[word_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[word_order] WITH NOCHECK ADD 
	CONSTRAINT [PK_word_order] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[word_order_quiz] WITH NOCHECK ADD 
	CONSTRAINT [PK_word_order_quiz] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[word_quiz] WITH NOCHECK ADD 
	CONSTRAINT [PK_word_quiz] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[account_verb] WITH NOCHECK ADD 
	CONSTRAINT [PK_account_verb] PRIMARY KEY  CLUSTERED 
	(
		[account_id],
		[verb_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[film_part] WITH NOCHECK ADD 
	CONSTRAINT [PK_film_part] PRIMARY KEY  CLUSTERED 
	(
		[film_id],
		[number]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[fm_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_fm_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[fm_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[tv_language] WITH NOCHECK ADD 
	CONSTRAINT [PK_tv_language] PRIMARY KEY  CLUSTERED 
	(
		[language_id],
		[tv_id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[level] WITH NOCHECK ADD 
	CONSTRAINT [DF_level_correct] DEFAULT (1) FOR [correct],
	CONSTRAINT [DF_level_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[level_quiz] WITH NOCHECK ADD 
	CONSTRAINT [DF_level_quiz_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[account_word] WITH NOCHECK ADD 
	CONSTRAINT [DF_account_word_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[article] WITH NOCHECK ADD 
	CONSTRAINT [DF_article_native_language_id] DEFAULT (1) FOR [native_language_id],
	CONSTRAINT [DF_article_learn_language_id] DEFAULT (2) FOR [learn_language_id]
GO

ALTER TABLE [dbo].[film] WITH NOCHECK ADD 
	CONSTRAINT [DF_film_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[fm] WITH NOCHECK ADD 
	CONSTRAINT [DF_fm_imageUrl] DEFAULT ('') FOR [image_url],
	CONSTRAINT [DF_fm_use_media_player] DEFAULT (1) FOR [use_media_player],
	CONSTRAINT [DF_fm_language_id] DEFAULT (1) FOR [language_id]
GO

ALTER TABLE [dbo].[tv] WITH NOCHECK ADD 
	CONSTRAINT [DF_tv_imageUrl] DEFAULT ('') FOR [image_url],
	CONSTRAINT [DF_tv_language_id] DEFAULT (1) FOR [language_id]
GO

ALTER TABLE [dbo].[word_order] WITH NOCHECK ADD 
	CONSTRAINT [DF_word_order_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[word_order_quiz] WITH NOCHECK ADD 
	CONSTRAINT [DF_word_order_quiz_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[word_quiz] WITH NOCHECK ADD 
	CONSTRAINT [DF_word_quiz_language_id] DEFAULT (2) FOR [language_id]
GO

ALTER TABLE [dbo].[fm_language] WITH NOCHECK ADD 
	CONSTRAINT [DF_fm_language_language_id] DEFAULT (1) FOR [language_id],
	CONSTRAINT [DF_fm_language_description] DEFAULT ('') FOR [description]
GO

ALTER TABLE [dbo].[tv_language] WITH NOCHECK ADD 
	CONSTRAINT [DF_tv_language_language_id] DEFAULT (1) FOR [language_id],
	CONSTRAINT [DF_tv_language_description] DEFAULT ('') FOR [description]
GO

 CREATE  UNIQUE  INDEX [IX_account_nick] ON [dbo].[account]([nick]) WITH  IGNORE_DUP_KEY  ON [PRIMARY]
GO

 CREATE  INDEX [IX_article_category_language_name] ON [dbo].[article_category_language]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_domain_word_language_name] ON [dbo].[domain_word_language]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_film_category_language_name] ON [dbo].[film_category_language]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_word_language_text] ON [dbo].[word_language]([text]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_fm_language_name] ON [dbo].[fm_language]([name]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_tv_language_name] ON [dbo].[tv_language]([name]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[account_word] ADD 
	CONSTRAINT [FK_account_word_account] FOREIGN KEY 
	(
		[account_id]
	) REFERENCES [dbo].[account] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_account_word_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_account_word_word] FOREIGN KEY 
	(
		[word_id]
	) REFERENCES [dbo].[word] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[article] ADD 
	CONSTRAINT [FK_article_category] FOREIGN KEY 
	(
		[category_id]
	) REFERENCES [dbo].[category] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_article_language] FOREIGN KEY 
	(
		[learn_language_id]
	) REFERENCES [dbo].[language] (
		[id]
	),
	CONSTRAINT [FK_article_language1] FOREIGN KEY 
	(
		[native_language_id]
	) REFERENCES [dbo].[language] (
		[id]
	),
	CONSTRAINT [FK_article_site] FOREIGN KEY 
	(
		[site_id]
	) REFERENCES [dbo].[site] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[article_category_language] ADD 
	CONSTRAINT [FK_article_category_language_category] FOREIGN KEY 
	(
		[category_id]
	) REFERENCES [dbo].[category] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_article_category_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[domain_word] ADD 
	CONSTRAINT [FK_domain_word_domain] FOREIGN KEY 
	(
		[domain_id]
	) REFERENCES [dbo].[domain] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_domain_word_word] FOREIGN KEY 
	(
		[word_id]
	) REFERENCES [dbo].[word] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[domain_word_language] ADD 
	CONSTRAINT [FK_domain_word_language_domain] FOREIGN KEY 
	(
		[domain_id]
	) REFERENCES [dbo].[domain] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_domain_word_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[film] ADD 
	CONSTRAINT [FK_film_film_category] FOREIGN KEY 
	(
		[film_category_id]
	) REFERENCES [dbo].[film_category] (
		[id]
	),
	CONSTRAINT [FK_film_film_player] FOREIGN KEY 
	(
		[film_player_id]
	) REFERENCES [dbo].[film_player] (
		[id]
	),
	CONSTRAINT [FK_film_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[film_category_language] ADD 
	CONSTRAINT [FK_film_category_language_film_category] FOREIGN KEY 
	(
		[film_category_id]
	) REFERENCES [dbo].[film_category] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_film_category_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[fm] ADD 
	CONSTRAINT [FK_fm_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[tv] ADD 
	CONSTRAINT [FK_tv_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[verb] ADD 
	CONSTRAINT [FK_verb_verb_type] FOREIGN KEY 
	(
		[verb_type_id]
	) REFERENCES [dbo].[verb_type] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[word_language] ADD 
	CONSTRAINT [FK_word_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_word_language_word] FOREIGN KEY 
	(
		[word_id]
	) REFERENCES [dbo].[word] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[word_order] ADD 
	CONSTRAINT [FK_word_order_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[word_order_quiz] ADD 
	CONSTRAINT [FK_word_order_quiz_account] FOREIGN KEY 
	(
		[account_id]
	) REFERENCES [dbo].[account] (
		[id]
	),
	CONSTRAINT [FK_word_order_quiz_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[word_quiz] ADD 
	CONSTRAINT [FK_word_quiz_account] FOREIGN KEY 
	(
		[account_id]
	) REFERENCES [dbo].[account] (
		[id]
	),
	CONSTRAINT [FK_word_quiz_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[account_verb] ADD 
	CONSTRAINT [FK_account_verb_account] FOREIGN KEY 
	(
		[account_id]
	) REFERENCES [dbo].[account] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_account_verb_verb] FOREIGN KEY 
	(
		[verb_id]
	) REFERENCES [dbo].[verb] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

ALTER TABLE [dbo].[film_part] ADD 
	CONSTRAINT [FK_film_part_film] FOREIGN KEY 
	(
		[film_id]
	) REFERENCES [dbo].[film] (
		[id]
	)
GO

ALTER TABLE [dbo].[fm_language] ADD 
	CONSTRAINT [FK_fm_language_fm] FOREIGN KEY 
	(
		[fm_id]
	) REFERENCES [dbo].[fm] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE ,
	CONSTRAINT [FK_fm_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	)
GO

ALTER TABLE [dbo].[tv_language] ADD 
	CONSTRAINT [FK_tv_language_language] FOREIGN KEY 
	(
		[language_id]
	) REFERENCES [dbo].[language] (
		[id]
	),
	CONSTRAINT [FK_tv_language_tv] FOREIGN KEY 
	(
		[tv_id]
	) REFERENCES [dbo].[tv] (
		[id]
	) ON DELETE CASCADE  ON UPDATE CASCADE 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_word_order_get_random
(
    @language_id int
)
AS

SELECT TOP 1 sentence
FROM word_order
WHERE language_id = @language_id
ORDER BY NEWID()


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_order_get_random]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_account_get
(
    @nick nvarchar(20),
    @password nvarchar(20) = NULL
)
AS

SELECT id, nick, email, password
FROM account
WHERE nick = @nick
AND (password IS NULL OR password = @password) 


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_account_get_count
AS

SELECT count(*) as cnt
FROM account


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_get_count]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_account_ins
(
    @nick nvarchar(20),
    @email nvarchar(50) = NULL,
    @password nvarchar(20) = NULL
)
AS

INSERT INTO account
(nick, email, password)
VALUES (@nick, @email, @password)

SELECT SCOPE_IDENTITY()


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_account_upd
(
    @id int,
    @nick nvarchar(20),
    @email nvarchar(50) = NULL,
    @password nvarchar(20) = NULL
)
AS

UPDATE account
SET nick = @nick, email = @email, password = @password
WHERE id=@id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_upd]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_language_get_list
AS

SELECT id, [name]
FROM [language]
ORDER BY [name]


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_language_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE pr_level_get_random
(
    @language_id int
)
AS

SELECT TOP 1 sentence, answer1, answer2, answer3, correct
FROM level
WHERE language_id = @language_id
ORDER BY NEWID()
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_level_get_random]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE pr_level_quiz_get_place
(
    @account_id int = null,
    @nick nvarchar(20),
    @success_count int,
    @language_id int
)
AS

INSERT INTO level_quiz(account_id, nick, result, language_id)
VALUES(@account_id, @nick, @success_count, @language_id)

DECLARE @id int
SET @id = (SELECT SCOPE_IDENTITY())

DECLARE @places TABLE
(
    place int IDENTITY (1, 1) NOT NULL,
    id int,
    account_id int,
    nick nvarchar(20),
    result int,
    language_id int
)

INSERT INTO @places (id, account_id, nick, result, language_id)
SELECT id, account_id, nick, result, language_id
FROM level_quiz
WHERE language_id = @language_id
ORDER BY result DESC, id DESC

SELECT TOP 1 place
FROM @places
WHERE id = @id

DELETE lq
FROM level_quiz lq
INNER JOIN @places p
ON lq.id = p.id
WHERE p.place > 100 AND lq.language_id = @language_id
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_level_quiz_get_place]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE pr_level_quiz_get_top
(
    @language_id int
)
AS

SELECT TOP 100
ISNULL(a.nick, q.nick) as nick,
q.result as result
FROM level_quiz q
LEFT JOIN account a
ON a.[id] = q.account_id
WHERE q.language_id = @language_id
ORDER BY result DESC, q.id DESC
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_level_quiz_get_top]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_site_get
AS

SELECT id, url
FROM site
ORDER BY id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_site_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_verb_type_get_list
AS

SELECT id, [rule]
FROM verb_type
ORDER BY id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_verb_type_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_word_get_count_sounded
AS

SELECT COUNT(*) AS [count] FROM word


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_count_sounded]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_sound_by_id
(
    @id int
)
AS

SELECT NULL AS original_sound, NULL AS translation_sound
FROM word WHERE id = @id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_sound_by_id]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_del
(
    @account_id int,
    @word_id int,
    @language_id int
)
AS

DELETE FROM account_word
WHERE account_id = @account_id
AND word_id = @word_id
AND language_id = @language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_del]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_get
(
    @account_id int,
    @word_id int,
    @language_id int
)
AS

SELECT account_id FROM account_word
WHERE account_id = @account_id
AND word_id = @word_id
AND language_id = @language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_get_count
(
    @account_id int,
    @language_id int
)
AS

SELECT COUNT(*) AS [count] FROM account_word
WHERE account_id = @account_id AND language_id = @language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_get_count]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_get_list
(
    @account_id int,
    @language_id int
)
AS

SELECT w.id, wl.text AS original
FROM word w
INNER JOIN account_word aw
ON aw.word_id = w.id
INNER JOIN word_language wl
ON wl.word_id = w.id
WHERE aw.account_id = @account_id
AND aw.language_id = @language_id
AND wl.language_id = @language_id
ORDER BY wl.text


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_get_top
(
    @language_id int
)
AS

SELECT TOP 20 a.nick as nick, COUNT(word_id) as [count]
FROM account_word aw
INNER JOIN account a
ON a.[id] = aw.account_id
WHERE aw.language_id = @language_id
GROUP BY a.nick
ORDER BY COUNT(word_id) DESC


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_get_top]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_get_top_20
AS

SELECT TOP 20 a.nick as nick, COUNT(word_id) as [count]
FROM account_word aw
INNER JOIN account a
ON a.[id] = aw.account_id
GROUP BY a.nick
ORDER BY COUNT(word_id) DESC


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_get_top_20]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_word_ins
(
    @account_id int,
    @word_id int,
    @language_id int
)
AS

IF (NOT EXISTS(SELECT * FROM account_word WHERE account_id = @account_id AND word_id = @word_id AND language_id = @language_id))
    BEGIN
        INSERT INTO account_word (account_id, word_id, language_id)
        VALUES (@account_id, @word_id, @language_id)
    END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_word_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_article_get
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT id, title, category_id
FROM article
WHERE native_language_id = @native_language_id
AND learn_language_id = @learn_language_id
ORDER BY [order]


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_article_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_article_get_by_id
(
    @id int
)
AS

SELECT a.title, a.body, s.url as site_url
FROM article a
inner join site s
on a.site_id = s.id
where a.id = @id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_article_get_by_id]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_category_get
(
    @language_id int
)
AS

SELECT category_id AS id, [name]
FROM article_category_language
WHERE language_id = @language_id
ORDER BY category_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_category_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_domain_get
(
    @language_id int
)
AS

SELECT d.[id], (dwl.[name] + ' (' + CAST(COUNT(dw.word_id) AS VARCHAR(10)) + ')') AS [name]
FROM domain d
INNER JOIN domain_word_language dwl
ON dwl.domain_id = d.id AND dwl.language_id = @language_id
LEFT JOIN domain_word dw 
ON d.[id] = dw.domain_id
GROUP BY dw.domain_id, d.[id], dwl.[name]
ORDER BY d.[id]


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_domain_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_film_category_get_list
(
    @language_id int
)
AS

SELECT film_category_id AS id, [name]
FROM [film_category_language]
WHERE language_id = @language_id
ORDER BY [name]


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_category_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE  PROCEDURE dbo.pr_film_get_list_by_category
(
    @category_id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT f.id, f.name AS [name], fcl.name AS category
FROM  film f
INNER JOIN film_category c
ON f.film_category_id = c.id
INNER JOIN film_category_language fcl
ON fcl.film_category_id = c.id AND fcl.language_id = @native_language_id
WHERE f.status = 1 AND (@category_id = 0 OR c.id = @category_id) AND f.language_id = @learn_language_id
ORDER BY f.name

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_get_list_by_category]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE  PROCEDURE dbo.pr_film_get_list_by_search
(
    @search nvarchar(10),
    @native_language_id int,
    @learn_language_id int
)
AS

IF (LEN(@search) <= 10)
    BEGIN
        SELECT f.id, f.name AS film, fcl.name AS category
        FROM  film f
        INNER JOIN film_category c
        ON f.film_category_id = c.id
        INNER JOIN film_category_language fcl
        ON fcl.film_category_id = c.id AND fcl.language_id = @native_language_id
        WHERE f.status = 1 AND f.name LIKE '%' + @search + '%' AND f.language_id = @learn_language_id
        ORDER BY f.name
    END

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_get_list_by_search]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_film_ins
(
    @name nvarchar(100),
    @category nvarchar(30),
    @player_pattern nvarchar(2000),
    @url nvarchar(100),
    @id int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @category_id int
DECLARE @player_id int

SET @category_id = (SELECT film_category_id
                    FROM film_category_language
                    WHERE [name] = @category AND
                    language_id = @native_language_id)
IF (@category_id IS NULL)
    BEGIN
        INSERT INTO film_category DEFAULT VALUES
        SET @category_id = SCOPE_IDENTITY()
        INSERT INTO film_category_language
        (language_id, film_category_id, [name])
        VALUES (@native_language_id, @category_id, @category)
    END

SET @player_id = (SELECT id FROM film_player WHERE pattern = @player_pattern)
IF (@player_id IS NULL)
    BEGIN
        INSERT INTO film_player (pattern) VALUES (@player_pattern)
        SET @player_id = SCOPE_IDENTITY()
    END

INSERT INTO film
([id], [name], [url], [film_category_id], [film_player_id], language_id)
VALUES (@id, @name, @url, @category_id, @player_id, @learn_language_id)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.pr_film_upd_status
(
    @id int,
    @status bit
)
AS
UPDATE film SET status = @status WHERE id = @id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_upd_status]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_fm_get
(
    @id int,
    @native_language_id int
)
AS

SELECT f.url, f.use_media_player, fl.description
FROM  fm f
INNER JOIN fm_language fl
ON fl.fm_id = f.id AND fl.language_id = @native_language_id
WHERE f.id = @id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_fm_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_fm_get_list
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT f.id, f.url, f.image_url, f.use_media_player, fl.name, fl.description
FROM  fm f
INNER JOIN fm_language fl
ON fl.fm_id = f.id AND fl.language_id = @native_language_id
WHERE f.language_id = @learn_language_id
ORDER BY f.id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_fm_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_tv_get
(
    @id int,
    @native_language_id int
)
AS

SELECT t.url, tl.description
FROM  tv t
INNER JOIN tv_language tl
ON tl.tv_id = t.id AND tl.language_id = @native_language_id
WHERE t.id = @id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_tv_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_tv_get_list
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT t.id, t.url, t.image_url, tl.name, tl.description
FROM  tv t
INNER JOIN tv_language tl
ON tl.tv_id = t.id AND tl.language_id = @native_language_id
WHERE t.language_id = @learn_language_id
ORDER BY t.id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_tv_get_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_by_domain
(
    @account_id int,
    @domain_id int,
    @word_count int,
    @word_start_index int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word TABLE
(
    number int IDENTITY (1, 1) NOT NULL,
    id int
)

INSERT INTO @word (id)
SELECT w.id
FROM word w
INNER JOIN domain_word dw
ON w.id = dw.word_id AND dw.domain_id = @domain_id
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @learn_language_id
WHERE w.id NOT IN
    (SELECT word_id FROM account_word
     WHERE account_id = @account_id AND
     language_id = @learn_language_id)
ORDER BY wl.text

SELECT ww.word_id AS id, ww.text AS original, www.text AS [translation]
FROM @word w
INNER JOIN word_language ww
ON w.id = ww.word_id AND ww.language_id = @learn_language_id
INNER JOIN word_language www
ON w.id = www.word_id AND www.language_id = @native_language_id
AND w.number >= @word_start_index
AND w.number <  @word_start_index + @word_count
ORDER BY w.number


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_by_domain]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_by_id
(
    @id int,
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT ww.[text] AS original, www.[text] AS [translation]
FROM word_language ww
INNER JOIN word_language www
ON ww.word_id = www.word_id
AND ww.word_id = @id
AND ww.language_id = @learn_language_id
AND www.language_id = @native_language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_by_id]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_by_ordered
(
    @account_id int,
    @word_count int,
    @word_start_index int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word TABLE
(
    number int IDENTITY (1, 1) NOT NULL,
    id int
)

INSERT INTO @word (id)
SELECT w.id
FROM word w
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @learn_language_id
INNER JOIN word_language wl2
ON w.id = wl2.word_id AND wl2.language_id = @native_language_id
WHERE w.id NOT IN
    (SELECT word_id FROM account_word
     WHERE account_id = @account_id AND
     language_id = @learn_language_id)
ORDER BY wl.text

SELECT ww.word_id AS id, ww.[text] AS original, www.text AS [translation]
FROM @word w
INNER JOIN word_language ww
ON w.id = ww.word_id AND ww.language_id = @learn_language_id
INNER JOIN word_language www
ON w.id = www.word_id AND www.language_id = @native_language_id
AND w.number >= @word_start_index
AND w.number <  @word_start_index + @word_count
ORDER BY w.number


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_by_ordered]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_by_original
(
    @original nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

SET @word_id = (SELECT TOP 1 word_id
                FROM word_language
                WHERE language_id = @learn_language_id AND
                [text] = @original)

SELECT @word_id AS id, [text] AS [translation], 1 AS sounded
FROM word_language
WHERE language_id = @native_language_id AND word_id = @word_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_by_original]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_by_random
(
    @account_id int,
    @word_count int,
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word TABLE
(
    number int IDENTITY (1, 1) NOT NULL,
    id int
)

INSERT INTO @word (id)
SELECT w.id
FROM word w
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @learn_language_id
INNER JOIN word_language wl2
ON w.id = wl2.word_id AND wl2.language_id = @native_language_id
WHERE w.id <= 18630 AND (w.id NOT IN
    (SELECT word_id
     FROM account_word
     WHERE account_id = @account_id AND
     language_id = @learn_language_id))
ORDER BY NEWID()

SELECT ww.word_id AS id, ww.text AS original, www.text AS [translation]
FROM @word w
INNER JOIN word_language ww
ON w.id = ww.word_id AND ww.language_id = @learn_language_id
INNER JOIN word_language www
ON w.id = www.word_id AND www.language_id = @native_language_id
AND w.number <= @word_count
ORDER BY w.number


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_by_random]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_word_get_count
(
    @learn_language_id int,
    @native_language_id int
)
AS

declare @count1 int
set @count1 = (
	SELECT COUNT(*)
	FROM word_language
	WHERE language_id = @learn_language_id)

declare @count2 int
set @count2 = (
	SELECT COUNT(*)
	FROM word_language
	WHERE language_id = @native_language_id)

IF @count1 < @count2
SELECT @count1
ELSE 
SELECT @count2


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_count]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_id_by_original
(
    @original nvarchar(254),
    @language_id int
)
AS

SELECT word_id AS id
FROM word_language
WHERE language_id = @language_id AND
[text] = @original


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_id_by_original]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_id_by_text
(
    @text nvarchar(254),
    @language_id int
)
AS

SELECT TOP 1 word_id AS id
FROM word_language
WHERE language_id = @language_id AND
[text] = @text


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_id_by_text]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_get_list_unsounded_short
(
    @native_language_id int,
    @learn_language_id int
)
AS

SELECT DISTINCT w.id--, wl.text, wwl.text
FROM word w
INNER JOIN word_language wl
ON w.id = wl.word_id AND wl.language_id = @native_language_id
INNER JOIN word_language wwl
ON w.id = wwl.word_id AND wwl.language_id = @learn_language_id
WHERE w.[id] > 18656
AND LEN(wl.text) < 21
AND CHARINDEX('''', wl.text) = 0
AND CHARINDEX('-', wl.text) = 0
AND CHARINDEX(' ', wl.text) = 0
AND CHARINDEX('.', wl.text) = 0
AND LEN(wwl.text) < 21
AND CHARINDEX('''', wwl.text) = 0
AND CHARINDEX('-', wwl.text) = 0
AND CHARINDEX(' ', wwl.text) = 0
AND CHARINDEX('.', wwl.text) = 0
ORDER BY w.[id]


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_get_list_unsounded_short]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_ins
(
    @original nvarchar(254),
    @translation nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

INSERT INTO word DEFAULT VALUES
SET @word_id = SCOPE_IDENTITY()

INSERT INTO word_language
(language_id, word_id, [text])
VALUES (@learn_language_id, @word_id, @original)

IF (@native_language_id <> @learn_language_id)
    BEGIN
        INSERT INTO word_language
        (language_id, word_id, [text])
        VALUES (@native_language_id, @word_id, @translation)
    END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_ins_use_exist
(
    @original nvarchar(254),
    @translation nvarchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

DECLARE @word_id int

SET @word_id = (
    SELECT TOP 1 word_id
    FROM word_language
    WHERE language_id = @learn_language_id
    AND text = @original)
    
IF (@word_id IS NOT NULL)
    BEGIN
        INSERT INTO word_language
        (language_id, word_id, [text])
        VALUES (@native_language_id, @word_id, @translation)
    END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_ins_use_exist]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE pr_word_order_quiz_get_place
(
    @account_id int = null,
    @nick nvarchar(20),
    @success_count int,
    @language_id int
)
AS

IF ((SELECT COUNT(*)  FROM word_order_quiz) >= 100) DELETE FROM word_order_quiz

INSERT INTO word_order_quiz(account_id, nick, result, language_id)
VALUES(@account_id, @nick, @success_count, @language_id)

DECLARE @id int
SET @id = (SELECT SCOPE_IDENTITY())

DECLARE @places TABLE
(
    place int IDENTITY (1, 1) NOT NULL,
    id int,
    account_id int,
    nick nvarchar(20),
    result int,
    language_id int
)

INSERT INTO @places (id, account_id, nick, result, language_id)
SELECT id, account_id, nick, result, language_id
FROM word_order_quiz
WHERE language_id = @language_id
ORDER BY result DESC, id DESC

SELECT TOP 1 place
FROM @places
WHERE id = @id

DELETE wq
FROM word_order_quiz wq
INNER JOIN @places p
ON wq.id = p.id
WHERE p.place > 100 AND wq.language_id = @language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_order_quiz_get_place]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_word_order_quiz_get_top
(
    @language_id int
)
AS

SELECT TOP 100
ISNULL(a.nick, q.nick) as nick,
q.result as result
FROM word_order_quiz q
LEFT JOIN account a
ON a.[id] = q.account_id
WHERE q.language_id = @language_id
ORDER BY result DESC, q.id DESC


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_order_quiz_get_top]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_word_quiz_get_place
(
    @account_id int = null,
    @nick nvarchar(20),
    @success_count int,
    @language_id int
)
AS

INSERT INTO word_quiz(account_id, nick, result, language_id)
VALUES(@account_id, @nick, @success_count, @language_id)

DECLARE @id int
SET @id = (SELECT SCOPE_IDENTITY())

DECLARE @places TABLE
(
    place int IDENTITY (1, 1) NOT NULL,
    id int,
    account_id int,
    nick nvarchar(20),
    result int,
    language_id int
)

INSERT INTO @places (id, account_id, nick, result, language_id)
SELECT id, account_id, nick, result, language_id
FROM word_quiz
WHERE language_id = @language_id
ORDER BY result DESC, id DESC

SELECT TOP 1 place
FROM @places
WHERE id = @id

DELETE wq
FROM word_quiz wq
INNER JOIN @places p
ON wq.id = p.id
WHERE p.place > 100 AND wq.language_id = @language_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_quiz_get_place]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



CREATE  PROCEDURE pr_word_quiz_get_top
(
    @language_id int
)
AS

SELECT TOP 100
ISNULL(a.nick, q.nick) as nick,
q.result as result
FROM word_quiz q
LEFT JOIN account a
ON a.[id] = q.account_id
WHERE q.language_id = @language_id
ORDER BY result DESC, q.id DESC



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_quiz_get_top]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_word_upd
(
    @id int,
    @original varchar(254),
    @translation varchar(254),
    @native_language_id int,
    @learn_language_id int
)
AS

UPDATE word_language
SET [text] = @original
WHERE word_id = @id AND language_id = @learn_language_id

IF (@native_language_id <> @learn_language_id)
    BEGIN
        UPDATE word_language
        SET [text] = @translation
        WHERE word_id = @id AND language_id = @native_language_id
    END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_word_upd]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_verb_del_list
(
    @account_id int
)
AS

DELETE FROM account_verb
WHERE account_id = @account_id


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_verb_del_list]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_verb_get_info
(
    @account_id int
)
AS
DECLARE @iKnow int
DECLARE @percentOfPopular int
DECLARE @percentOfTotal int

SET @iKnow = (SELECT COUNT(*) FROM account_verb
WHERE account_id = @account_id)

SET @percentOfPopular = (SELECT CEILING(100 * @iKnow / COUNT(*)) FROM verb
WHERE popular = 1)

IF (@percentOfPopular > 100) SET @percentOfPopular = 100

SET @percentOfTotal = (SELECT CEILING(100 * @iKnow / COUNT(*)) FROM verb)

SELECT
    @iKnow AS iKnow,
    @percentOfPopular AS percentOfPopular,
    @percentOfTotal AS percentOfTotal


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_verb_get_info]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_verb_get_top
AS

SELECT TOP 20 a.nick as nick, COUNT(verb_id) as [count]
FROM account_verb av
INNER JOIN account a
ON a.[id] = av.account_id
GROUP BY a.nick
ORDER BY COUNT(verb_id) DESC


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_verb_get_top]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_verb_get_top_20
AS

SELECT TOP 20 a.nick as nick, COUNT(verb_id) as [count]
FROM account_verb av
INNER JOIN account a
ON a.[id] = av.account_id
GROUP BY a.nick
ORDER BY COUNT(verb_id) DESC


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_verb_get_top_20]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_account_verb_ins
(
    @account_id int,
    @verb_id int
)
AS

IF (NOT EXISTS(SELECT * FROM account_verb WHERE account_id = @account_id AND verb_id = @verb_id))
    BEGIN
        INSERT INTO account_verb (account_id, verb_id)
        VALUES (@account_id, @verb_id)
    END


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_account_verb_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_film_clear_all
AS

DELETE FROM film_part
DELETE FROM film
DELETE FROM film_player
DELETE FROM film_category_language
DELETE FROM film_category


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_clear_all]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_film_get
(
    @id int
)
AS

SELECT f.id, f.name AS [name], f.url AS url, p.pattern AS pattern
FROM  film f
INNER JOIN film_player p
ON f.film_player_id = p.id
WHERE f.id = @id

SELECT url
FROM  film_part
WHERE film_id = @id
ORDER BY number


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_get]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE dbo.pr_film_part_ins
(
    @url nvarchar(100),
    @id int,
    @number int
)
AS

INSERT INTO film_part
(film_id, number, url)
VALUES (@id, @number, @url)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_film_part_ins]  TO [wordz_users]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


CREATE PROCEDURE pr_verb_get_list
(
    @account_id int,
    @load_popular bit,
    @not_use_well_known_verbs bit,
    @word_count int
)
AS

SET RowCount @word_count
SELECT v.id, v.form1, v.form2, v.form3, v.translation, v.verb_type_id
FROM verb v
LEFT JOIN account_verb av
ON av.verb_id = v.id AND av.account_id = @account_id
WHERE (@load_popular = 0 OR v.popular = 1) AND
(@not_use_well_known_verbs = 0 OR av.account_id IS NULL)
ORDER BY v.form1


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

GRANT  EXECUTE  ON [dbo].[pr_verb_get_list]  TO [wordz_users]
GO

