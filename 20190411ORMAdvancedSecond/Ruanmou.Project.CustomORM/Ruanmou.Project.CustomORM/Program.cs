using Ruanmou.DAL;
using Ruanmou.Libraries.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Project.CustomORM
{
    /// <summary>
    /// 1 从Ado.Net到通用数据库访问层
    /// 2 泛型+反射+Ado.Net完成通用主键查询&全表查询
    /// 3 特性attribute完成映射
    /// 
    /// 
    /// 手写ORM，跳槽季必备的东西，只会用但是不懂why
    /// 初步计划3次课，希望大家能保持关注
    /// 什么是ORM？  用过ORM的打个1  没有打个2
    /// 
    /// 对象关系映射，就是可以不用写sql，直接操作数据库的一个封装的类库
    /// 
    /// 
    /// 1 泛型+反射+Ado.Net完成通用数据插入
    /// 2 主键识别 防SQL注入 null处理
    /// 3 泛型缓存完成性能提升
    /// 4 委托完成代码重用
    /// 
    /// 昨天的内容就是泛型+反射+Ado.Net+特性，完成了通用数据库主键查询，支持映射的封装  
    /// 
    /// 自增主键不能插入赋值
    /// sql注入，参数化
    /// Insert Null式要换成DbNull.Value
    /// 
    /// 完成了泛型+反射+Ado.Net+特性，完成了通用数据库插入，支持映射，防注入，
    /// 100%  1
    /// 80%   2
    /// 60%   3
    /// 以下  4
    /// 
    /// 要完成一些通用的封装，考虑的东西是非常多的，细节也是非常多的，涉及的知识点更是相当多
    /// 
    /// 泛型缓存做性能优化，只要某个实体完成过任何操作，sql都会初始化完成并且缓存，下次再来的时候就可以直接用，交错使用没有影响
    /// 
    /// 1 泛型+反射+Ado.Net完成通用数据更新
    /// 2 委托完成代码重用
    /// 3 通用数据验证功能实现
    /// 4 表达式目录树完成批量更新
    /// 
    /// 今天是第三次课，手写ORM
    /// 
    /// 泛型+反射+Ado.Net+特性，完成了通用数据库主键查询，支持映射的封装 
    /// 完成了泛型+反射+Ado.Net+特性，完成了通用数据库插入，支持映射，防注入，泛型缓存性能优化
    /// 如果前两天课程没来的小伙伴儿，可以自行联系课堂助教老师，获取视频+代码
    /// 
    /// ADO.NET的操作，是重复的代码，做一下代码重用
    /// 
    /// 听到这里，内容已经非常多了，很需要大家下去消化了，
    /// 如果还觉得意犹未尽，还想接着深入扩展的，刷个1
    /// 
    /// 泛型+反射+Ado.Net+Attribute，完成了通用的数据库更新，支持映射 防注入 支持可空类型，泛型缓存优化性能 委托封装代码重用
    /// 小伙伴儿们加一下课堂的助教老师，获取一下三次课的视频+代码，好好的消化一下，如果有相关的知识点不太清白的，也可以找下助教老师
    /// 然后还有些小伙伴儿还想更进一步的，或者有什么想法和建议，欢迎跟Eleven交流，老师的QQ57265177，或者工作中的一些技术难题
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("欢迎来到.Net高级班体验课，今天Eleven老师要开启手写ORM专题了");
                #region 0409
                //{
                //    Console.WriteLine("*****************Ado基本操作*****************");
                //    ////1 操作数据库  Ado.Net
                //    //SqlHelper helper = new SqlHelper();
                //    //Company company = helper.FindCompany(1);

                //    ////User user=
                //}
                //{
                //    //2 一个方法完成不同的表的主键查询
                //    Console.WriteLine("*****************泛型+反射*****************");
                //    SqlHelper helper = new SqlHelper();
                //    Company company = helper.Find<Company>(1);
                //    UserModel user = helper.Find<UserModel>(1);
                //    Company company8 = helper.Find<Company>(8);
                //}
                //{
                //    //3 数据库的字段跟程序中可能不一致？！
                //    Console.WriteLine("*****************attribute完成映射*****************");
                //}
                #endregion

                #region 0410
                {
                    //Console.WriteLine("***************泛型+反射通用数据插入***************");
                    //SqlHelper helper = new SqlHelper();
                    //bool bResult1 = helper.Insert<Company>(new Company()
                    //{
                    //    Name = "软谋教育2",
                    //    CreateTime = DateTime.Now,
                    //    CreatorId = 123,
                    //    //LastModifierId = 234,
                    //    //LastModifyTime = DateTime.Now.AddDays(-5)
                    //});

                    //bool bResult2 = helper.Insert<Company>(new Company()
                    //{
                    //    Name = "软谋教育2",
                    //    CreateTime = DateTime.Now,
                    //    CreatorId = 123,
                    //    //LastModifierId = 234,
                    //    //LastModifyTime = DateTime.Now.AddDays(-5)
                    //});


                    //UserModel user = helper.Find<UserModel>(1);
                    //user.Name = "做自己";
                    //helper.Insert<UserModel>(user);

                    //bool bResult3 = helper.Insert<Company>(new Company()
                    //{
                    //    Name = "软谋教育3",
                    //    CreateTime = DateTime.Now,
                    //    CreatorId = 123,
                    //    //LastModifierId = 234,
                    //    //LastModifyTime = DateTime.Now.AddDays(-5)
                    //});
                    //user.Name = "孟青林";
                    //helper.Insert<UserModel>(user);

                    //bool bResult4 = helper.Insert<Company>(new Company()
                    //{
                    //    Name = "软谋教育4",
                    //    CreateTime = DateTime.Now,
                    //    CreatorId = 123,
                    //    //LastModifierId = 234,
                    //    //LastModifyTime = DateTime.Now.AddDays(-5)
                    //});

                    //user.Name = "Z";
                    //helper.Insert<UserModel>(user);
                }
                #endregion

                #region 0411
                {
                    Console.WriteLine("**************通用数据库更新**************");
                    SqlHelper helper = new SqlHelper();
                    helper.Insert(new Company()
                    {
                        Name = "tencent",
                        CreateTime = DateTime.Now,
                        CreatorId = 1,
                    });


                    Company company = helper.Find<Company>(8);
                    company.Name = "软谋教育集团";
                    helper.Update<Company>(company);

                    UserModel userModel = helper.Find<UserModel>(1);
                    userModel.Name = "闪亮crystal";
                    userModel.LastModifyTime = DateTime.Now;
                    helper.Update<UserModel>(userModel);


                    company.Name = "软谋教育集团2";
                    helper.Update<Company>(company);

                    userModel.Name = "城管小队长";
                    userModel.LastModifyTime = DateTime.Now;
                    helper.Update<UserModel>(userModel);

                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
