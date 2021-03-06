
/*所属マスタ、性別マスタのデータ作成*/

USE crow_emp_sys_db;

GO

/*所属マスタにデータ挿入*/

INSERT INTO m_section VALUES('A1','管理部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('A2','総務部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('A3','人事部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('B1','企画部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('B2','営業部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('C1','技術部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('C2','設計部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('C3','開発部',GETDATE(),GETDATE());
INSERT INTO m_section VALUES('C4','品質管理部',GETDATE(),GETDATE());


/*性別マスタにデータ挿入*/

INSERT INTO m_gender VALUES('0','不明',GETDATE(),GETDATE());
INSERT INTO m_gender VALUES('1','男性',GETDATE(),GETDATE());
INSERT INTO m_gender VALUES('2','女性',GETDATE(),GETDATE());
INSERT INTO m_gender VALUES('9','その他',GETDATE(),GETDATE());

GO