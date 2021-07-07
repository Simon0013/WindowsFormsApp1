--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: clint_name(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.clint_name() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
if NEW.name IS NULL THEN
RAISE EXCEPTION 'error no name at column';
end if;
return new;
end;
$$;


ALTER FUNCTION public.clint_name() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: adresfirm; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.adresfirm (
    kodfirm integer NOT NULL,
    area character varying(30) NOT NULL,
    city character varying(30) NOT NULL,
    street character varying(30) NOT NULL,
    house character varying(10) NOT NULL,
    index character varying(7) NOT NULL
);


ALTER TABLE public.adresfirm OWNER TO postgres;

--
-- Name: adressotr; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.adressotr (
    kodsotrud integer NOT NULL,
    area character varying(30) NOT NULL,
    city character varying(30) NOT NULL,
    street character varying(30) NOT NULL,
    house character varying(10) NOT NULL,
    index character varying(7) NOT NULL
);


ALTER TABLE public.adressotr OWNER TO postgres;

--
-- Name: aktviprabot; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.aktviprabot (
    kodakta integer NOT NULL,
    date date NOT NULL,
    kodsotrud integer NOT NULL,
    kodclienta integer NOT NULL
);


ALTER TABLE public.aktviprabot OWNER TO postgres;

--
-- Name: client; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.client (
    kodclienta integer NOT NULL,
    fam character varying(25) NOT NULL,
    name character varying(25) NOT NULL,
    surname character varying(25) NOT NULL,
    firm_kodfirm integer NOT NULL
);


ALTER TABLE public.client OWNER TO postgres;

--
-- Name: firm; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.firm (
    kodfirm integer NOT NULL,
    nazvanfirm character varying(30) NOT NULL,
    timerabot character varying(15) NOT NULL,
    kodclienta integer NOT NULL,
    director character varying(35) NOT NULL
);


ALTER TABLE public.firm OWNER TO postgres;

--
-- Name: firm_info; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.firm_info AS
 SELECT adresfirm.kodfirm AS "id_фирмы",
    firm.nazvanfirm AS "название_фирмы",
    adresfirm.street AS "street_улица"
   FROM (public.firm
     JOIN public.adresfirm ON ((firm.kodfirm = adresfirm.kodfirm)));


ALTER TABLE public.firm_info OWNER TO postgres;

--
-- Name: rabota_po_akty; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.rabota_po_akty (
    kodakta integer NOT NULL,
    kodrabot integer NOT NULL,
    obiem_rabot character varying(25) NOT NULL
);


ALTER TABLE public.rabota_po_akty OWNER TO postgres;

--
-- Name: raboti; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.raboti (
    kodrabot integer NOT NULL,
    nazvanie character varying(30) NOT NULL,
    cena character varying(20) NOT NULL,
    kodsotrud integer NOT NULL
);


ALTER TABLE public.raboti OWNER TO postgres;

--
-- Name: sotrudniki; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.sotrudniki (
    kodsotrud integer NOT NULL,
    family character varying(30) NOT NULL,
    name character varying(30) NOT NULL,
    otchestvo character varying(30) NOT NULL,
    doljnost character varying(25) NOT NULL,
    adres character varying(99) NOT NULL,
    telephone character varying(11) NOT NULL,
    firm_kodfirm integer NOT NULL
);


ALTER TABLE public.sotrudniki OWNER TO postgres;

--
-- Name: sotrud_info; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.sotrud_info AS
 SELECT adressotr.kodsotrud AS "id_сотрудника",
    sotrudniki.family AS "family_сотрудника",
    adressotr.city AS "city_проживает"
   FROM (public.sotrudniki
     JOIN public.adressotr ON ((sotrudniki.kodsotrud = adressotr.kodsotrud)));


ALTER TABLE public.sotrud_info OWNER TO postgres;

--
-- Name: viprabot_sotrud; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.viprabot_sotrud AS
 SELECT aktviprabot.kodakta AS "код_акта",
    sotrudniki.family AS "фамилия",
    aktviprabot.date AS "дата",
    aktviprabot.kodclienta AS "код_клиента"
   FROM (public.aktviprabot
     JOIN public.sotrudniki ON ((aktviprabot.kodakta = sotrudniki.kodsotrud)));


ALTER TABLE public.viprabot_sotrud OWNER TO postgres;

--
-- Data for Name: adresfirm; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.adresfirm (kodfirm, area, city, street, house, index) FROM stdin;
1	Ростовская область	Ростов-на-дону	Гриши Волкова	36	360040
2	Ростовская область	Ростов-на-Дону	Казахская	87	360020
3	Ростовская область	Ростов-на-Дону	Пушкинская	25	360030
4	Ростовская область	Ростов-на-Дону	Большая садовая	36	360020
5	Ростовская область	Ростов-на-Дону	Стачки	74	360010
6	Ростовская область	Ростов-на-Дону	Ленина	55	360070
\.


--
-- Data for Name: adressotr; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.adressotr (kodsotrud, area, city, street, house, index) FROM stdin;
1	Ростовская область	Ростов-на-Дону	Московская	52	36040
2	Ростовская область	Ростов-на-Дону	Тургеневская	12	36050
3	Ростовская область	Ростов-на-Дону	2-ая Краснодарская	169	36070
4	Ростовская область	Ростов-на-Дону	ГПЗ	10	36080
5	Ростовская область	Ростов-на-Дону	Молодежная	22	36060
6	Ростовская область	Ростов-на-Дону	Мира	47	36030
\.


--
-- Data for Name: aktviprabot; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.aktviprabot (kodakta, date, kodsotrud, kodclienta) FROM stdin;
1	2021-05-03	1	1
2	2021-04-15	2	2
3	2021-05-19	3	3
4	2021-04-25	4	4
5	2021-05-19	5	5
6	2021-03-17	6	6
\.


--
-- Data for Name: client; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.client (kodclienta, fam, name, surname, firm_kodfirm) FROM stdin;
1	Погорельский	Павел	Сергеевич	1
2	Бакланов	Дмитрий	Иванович	2
3	Голубниченко	Георгий	Алексеевич	3
4	Мамыркин	Ярослав	Алексеевич	4
5	Нетребский	Александр	Сергеевич	5
6	Швариокин	Виталий	Георгиевич	6
7	Бутов	Андрей	Сергеевич	7
\.


--
-- Data for Name: firm; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.firm (kodfirm, nazvanfirm, timerabot, kodclienta, director) FROM stdin;
1	Ростсельмаш	12	1	Зотов
2	Донэнерго	8	2	Петров
3	Пик	17	3	Шевырев
4	Юит	14	4	Ступаков
5	Capital grou	16	5	Винник
6	Строймастер	12	6	Поларчан
7	Согаз	25	7	Петров
\.


--
-- Data for Name: rabota_po_akty; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.rabota_po_akty (kodakta, kodrabot, obiem_rabot) FROM stdin;
1	1	12
2	2	8
3	3	17
4	4	14
5	5	14
6	6	16
\.


--
-- Data for Name: raboti; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.raboti (kodrabot, nazvanie, cena, kodsotrud) FROM stdin;
1	монтажных работ на участке	500000	1
2	организует ремонтные работы	750000	2
3	управления работой Отдела	1000000	3
4	Организует условия труда 	30000	4
5	Организует табельный учет	80000	5
6	Выполняет план строительства	97000	6
\.


--
-- Data for Name: sotrudniki; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sotrudniki (kodsotrud, family, name, otchestvo, doljnost, adres, telephone, firm_kodfirm) FROM stdin;
1	Cухомлинов	Евгений	Сергеевич	Производитель работ	Стачки 169	89526076280	1
2	Куренкова	Мария	Дмитриевна	Начальник цеха	Труженников 45	89526075550	2
3	Аникина	Дарья	Дмитриевна	Начальник сметного отдела	Казахская 85	89526333550	3
4	Лиховидов	Глеб	Иванович	Начальник охраны труда	Гриши Волкова 36	89526123550	4
5	Беляев	Николай	Петрович	Начальник отдела кадров	Ленина 54	89526444550	5
6	Терещенко	Денис	Геннадьевич	Менеджер	Пушкинская 69	89526888550	6
\.


--
-- Name: adresfirm_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.adresfirm
    ADD CONSTRAINT adresfirm_pk PRIMARY KEY (kodfirm);


--
-- Name: adressotr_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.adressotr
    ADD CONSTRAINT adressotr_pk PRIMARY KEY (kodsotrud);


--
-- Name: aktviprabot_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.aktviprabot
    ADD CONSTRAINT aktviprabot_pk PRIMARY KEY (kodakta);


--
-- Name: client_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_pk PRIMARY KEY (kodclienta);


--
-- Name: firm_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.firm
    ADD CONSTRAINT firm_pk PRIMARY KEY (kodfirm);


--
-- Name: rabota_po_akty_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.rabota_po_akty
    ADD CONSTRAINT rabota_po_akty_pk PRIMARY KEY (kodakta, kodrabot);


--
-- Name: raboti_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.raboti
    ADD CONSTRAINT raboti_pk PRIMARY KEY (kodrabot);


--
-- Name: sotrudniki_pk; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.sotrudniki
    ADD CONSTRAINT sotrudniki_pk PRIMARY KEY (kodsotrud);


--
-- Name: clint_name; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER clint_name BEFORE INSERT OR UPDATE ON public.client FOR EACH ROW EXECUTE PROCEDURE public.clint_name();


--
-- Name: adresfirm_firm_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adresfirm
    ADD CONSTRAINT adresfirm_firm_fk FOREIGN KEY (kodfirm) REFERENCES public.firm(kodfirm);


--
-- Name: adressotr_sotrudniki_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.adressotr
    ADD CONSTRAINT adressotr_sotrudniki_fk FOREIGN KEY (kodsotrud) REFERENCES public.sotrudniki(kodsotrud);


--
-- Name: aktviprabot_client_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aktviprabot
    ADD CONSTRAINT aktviprabot_client_fk FOREIGN KEY (kodclienta) REFERENCES public.client(kodclienta);


--
-- Name: aktviprabot_sotrudniki_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.aktviprabot
    ADD CONSTRAINT aktviprabot_sotrudniki_fk FOREIGN KEY (kodsotrud) REFERENCES public.sotrudniki(kodsotrud);


--
-- Name: client_firm_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.client
    ADD CONSTRAINT client_firm_fk FOREIGN KEY (firm_kodfirm) REFERENCES public.firm(kodfirm);


--
-- Name: rab_po_akty_aktviprabot_fkv2; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rabota_po_akty
    ADD CONSTRAINT rab_po_akty_aktviprabot_fkv2 FOREIGN KEY (kodakta) REFERENCES public.aktviprabot(kodakta);


--
-- Name: rab_po_akty_raboti_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rabota_po_akty
    ADD CONSTRAINT rab_po_akty_raboti_fk FOREIGN KEY (kodrabot) REFERENCES public.raboti(kodrabot);


--
-- Name: raboti_sotrudniki_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.raboti
    ADD CONSTRAINT raboti_sotrudniki_fk FOREIGN KEY (kodsotrud) REFERENCES public.sotrudniki(kodsotrud);


--
-- Name: sotrudniki_firm_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sotrudniki
    ADD CONSTRAINT sotrudniki_firm_fk FOREIGN KEY (firm_kodfirm) REFERENCES public.firm(kodfirm);


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

