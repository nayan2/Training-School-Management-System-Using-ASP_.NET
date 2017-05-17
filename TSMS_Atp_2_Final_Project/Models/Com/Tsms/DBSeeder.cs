using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Globalization;
using System.Data.Entity.Validation;

namespace TSMS_Atp_2_Final_Project.Models.Com.Tsms
{
    public class DBSeeder : DropCreateDatabaseIfModelChanges<TsmsDBContext>
    {
        protected override void Seed(TsmsDBContext context)
        {
            ///ADD USERDETAILS.......
            context.UserDetails.Add(new UserDetail() {
                UserId = "13-2451922-2",
                first_name = "md.amjad",
                last_name = "hossain",
                fullname = "md.amjad hossain",
                pic_path = "~/Content/img/tsms/default/owner.jpg",
                company_name = "aiub",
                city = "dhaka",
                phone_number = 01830954149,
                email = "nayanchowdhury92@gmail.com",
                zip_code = 1362,
                nationality = "bangladeshi",
                sex = "male",
                religion = "islam",
                blood_group = "o+",
                dob = DateTime.Parse("1995-02-25 00:00:00.000"),
                user_activation_date = DateTime.Now,
                user_active = "active",
                User = new User()
                {
                    UserId = "13-2451922-2",
                    password = "admin",
                    level = "admin"
                }
            });

            context.UserDetails.Add(new UserDetail() {
                UserId = "13-24519-2",
                first_name = "md.amjad",
                last_name = "hossain",
                fullname = "md.amjad hossain",
                pic_path = "~/Content/img/tsms/default/owner.jpg",
                company_name = "aiub",
                city = "dhaka",
                phone_number = 01830954149,
                email = "nayanchowdhury92@gmail.com",
                zip_code = 1362,
                nationality = "bangladeshi",
                sex = "male",
                religion = "islam",
                blood_group = "o+",
                dob = DateTime.Parse("1995-02-25 00:00:00.000"),
                user_activation_date = DateTime.Now,
                user_active = "active",
                User = new User()
                {
                    UserId = "13-24519-2",
                    password = "student",
                    level = "student"
                }
            });
            /////ADD VENDORS..............
            context.Vendors.Add(new Vendor() {
                heading = "CISCO",
                pic_path = "~/Content/img/tsms/default/Cisco-Networking-Academy.jpg",
                adding_date = DateTime.Now,
                body = "Cisco Systems, Inc. (known as Cisco) is an American multinational technology conglomerate headquartered in San José, California, in the center of Silicon Valley, that develops, manufactures, and sells networking hardware, telecommunications equipment, and other high-technology services and products.[4] Through its numerous acquired subsidiaries, such as OpenDNS, WebEx, and Jasper, Cisco specializes into specific tech markets, such as Internet of Things (IoT), domain security, and energy management.",
            });

            context.Vendors.Add(new Vendor() {
                heading = "JUNIPER",
                pic_path = "~/Content/img/tsms/default/junifer.jpg",
                adding_date = DateTime.Now,
                body = "Juniper Networks is an American multinational corporation headquartered in Sunnyvale, California that develops and markets networking products. Its products include routers, switches, network management software, network security products and software-defined networking technology.",
            });

            context.Vendors.Add(new Vendor()
            {
                heading = "ORACLE",
                pic_path = "~/Content/img/tsms/default/oracle.png",
                adding_date = DateTime.Now,
                body = "Oracle Corporation is an American multinational computer technology corporation, headquartered in Redwood Shores, California. The company primarily specializes in developing and marketing database software and technology, cloud engineered systems and enterprise software productsâ€”particularly its own brands of database management systems. In 2015 Oracle was the second-largest software maker by revenue, after Microsoft.",
            });
            /////ADD COURSES..........
            context.Courses.Add(new Course()
            {
                name = "JNCSP-SEC",
                vendor_heading = "JUNIPER",
                code = "JNCSP-SEC 3032",
                pic_path = "~/Content/img/tsms/default/Juniper_JNCSP-SEC.png",
                adding_date = DateTime.Now,
                details = "The Juniper Networks Certification Program (JNCP) Junos Security certification track is a program that allows participants to demonstrate competence with Juniper Networks technology. Successful candidates demonstrate thorough understanding of security technology in general and Junos software for SRX Series devices."
            });

            context.Courses.Add(new Course()
            {
                name = "CCNA Security",
                vendor_heading = "CISCO",
                code = "CCNA-Security 3033",
                pic_path = "~/Content/img/tsms/default/Cisco_CCNA_Sec.png",
                adding_date = DateTime.Now,
                details = "Cisco Certified Network Associate Security (CCNA Security) validates associate-level knowledge and skills required to secure Cisco networks. With a CCNA Security certification, a network professional demonstrates the skills required to develop a security infrastructure, recognize threats and vulnerabilities to networks, and mitigate security threats."
            });

            context.Courses.Add(new Course()
            {
                name = "Oracle Database 12c",
                vendor_heading = "ORACLE",
                code = "Oracle Database 12c 3034",
                pic_path = "~/Content/img/tsms/default/oracle-cloud.jpg",
                adding_date = DateTime.Now,
                details = "Over successive releases of Oracle Database, Oracle continues to ease our customers' efforts to standardize, consolidate, and automate database services on the cloud. What started more than a decade ago with pioneering features like Oracle Real Application Clusters and Oracle Automatic Storage Management now continues with Oracle Multitenant, which enables IT to tap fully into the benefits of the cloud, including resource sharing, management flexibility, and cost savings."
            });

            ////ADD INSTRUCTORS................
            context.Instructors.Add(new Instructor()
            {
                pic_path = "~/Content/img/tsms/default/owner.jpg",
                first_name = "Imran",
                last_name = "chowdhury",
                faculty_name = "Imran chowdhury",
                company_name = "aiub",
                city = "dhaka",
                phone_number = 01516151495,
                email = "nayanchowdhury92@gmail.com",
                zip_code = 1362,
                nationality = "bangladeshi",
                sex = "male",
                religion = "islam",
                blood_group = "o+",
                dob = DateTime.Parse("1995-02-25 00:00:00.000"),
                faculty_activation_date = DateTime.Now,
                faculty_active = "active"
            });

            context.Instructors.Add(new Instructor()
            {
                pic_path = "~/Content/img/tsms/default/owner.jpg",
                first_name = "Nayan",
                last_name = "chowdhury",
                faculty_name = "Nayan chowdhury",
                company_name = "aiub",
                city = "dhaka",
                phone_number = 01830954149,
                email = "nayan2521995@gmail.com",
                zip_code = 1362,
                nationality = "bangladeshi",
                sex = "male",
                religion = "islam",
                blood_group = "a+ VE",
                dob = DateTime.Parse("1995-02-25 00:00:00.000"),
                faculty_activation_date = DateTime.Now,
                faculty_active = "active"
            });
            ////ADD BATCHES...............
            context.Batches.Add(new Batche()
            {
                name = "JNCSP-SEC",
                batch_code = "JNCSP-SEC-1",
                batch_starting_date = DateTime.Parse("2017-12-25 00:00:00.000"),
                admission_last_date = DateTime.Parse("2017-10-25 00:00:00.000"),
                room_number = 4042,
                faculty_name = "Nayan chowdhury",
                amount = 12000,
                details = "this is a juniper course.",
                routine = "Sunday-10:10 PM to 10:10 PM and Tuesday-10:10 PM to 10:10 PM"
            });

            context.Batches.Add(new Batche()
            {
                name = "CCNA Security",
                batch_code = "CCNA Security-1",
                batch_starting_date = DateTime.Parse("2017-05-01 00:00:00.000"),
                admission_last_date = DateTime.Parse("2017-04-20 00:00:00.000"),
                room_number = 4043,
                faculty_name = "Imran chowdhury",
                amount = 15000,
                details = "this is a ccna course.",
                routine = "Sunday-10:10 PM to 10:10 PM and Tuesday-10:10 PM to 10:10 PM"
            });

            context.Batches.Add(new Batche()
            {
                name = "CCNA Security",
                batch_code = "CCNA Security-2",
                batch_starting_date = DateTime.Parse("2017-08-25 00:00:00.000"),
                admission_last_date = DateTime.Parse("2017-08-02 00:00:00.000"),
                room_number = 5053,
                faculty_name = "Imran chowdhury",
                amount = 20000,
                details = "this is a ccna course.",
                routine = "Wednesday-10:10 PM to 10:10 PM"
            });

            context.Batches.Add(new Batche()
            {
                name = "Oracle Database 12c",
                batch_code = "Oracle Database 12c-1",
                batch_starting_date = DateTime.Parse("2017-05-03 00:00:00.000"),
                admission_last_date = DateTime.Parse("2017-04-10 00:00:00.000"),
                room_number = 5054,
                faculty_name = "Imran chowdhury",
                amount = 16000,
                details = "this is a oracle course.",
                routine = "Monday-10:10 PM to 10:10 PM"
            });
            ////ADD STUDENTASSIGNBATCHES..............
            context.Studentassignbatches.Add(new Studentassignbatche()
            {
                UserId = "13-24519-2",
                name = "CCNA Security",
                batch_code = "CCNA Security-1",
                status = "in progress"
            });

            context.Studentassignbatches.Add(new Studentassignbatche()
            {
                UserId = "13-24519-2",
                name = "Oracle Database 12c",
                batch_code = "Oracle Database 12c-1",
                status = "in progress"
            });
            ////ADD FINANCEOFSTUDENTS.........
            context.Financeofstudents.Add(new Financeofstudent() {
                UserId = "13-24519-2",
                batch_code = "Oracle Database 12c-1",
                debit = 16000,
                credit = 0,
                balance = 16000,
                lastTrunsaction = DateTime.Parse("2017-05-03 00:00:00.000")
            });

            context.Financeofstudents.Add(new Financeofstudent(){
                UserId = "13-24519-2",
                batch_code = "CCNA Security-1",
                debit = 15000,
                credit = 2000,
                balance = 13000,
                lastTrunsaction = DateTime.Parse("2017-05-02 00:00:00.000")
            });
            //////END OF ADDINGS................
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            base.Seed(context);
        }
    }
}