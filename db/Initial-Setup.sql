CREATE TABLE public.contacts
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    first_name text,
    last_name text,
    email text,
    when_created timestamp without time zone,
    CONSTRAINT contacts_pkey PRIMARY KEY (id),
    CONSTRAINT unique_email UNIQUE (email)
);

CREATE TABLE public.contact_audits
(
    id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    contact_id text,
    username text,
    change_description text,
    when_occurred timestamp without time zone,
    CONSTRAINT contact_audits_pkey PRIMARY KEY (id)
);
