
package com.mincom.enterpriseservice.ellipse.securityclass;

import java.net.MalformedURLException;
import java.net.URL;
import javax.jws.HandlerChain;
import javax.xml.namespace.QName;
import javax.xml.ws.Service;
import javax.xml.ws.WebEndpoint;
import javax.xml.ws.WebServiceClient;
import javax.xml.ws.WebServiceException;
import javax.xml.ws.WebServiceFeature;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 2.2.6-1b01 
 * Generated source version: 2.2
 * 
 */
@WebServiceClient(name = "SecurityClassService", targetNamespace = "http://securityclass.ellipse.enterpriseservice.mincom.com", wsdlLocation = "http://ews-el8desa.lmnerp03.cerrejon.com/ews/services/SecurityClassService?WSDL")
@HandlerChain(file = "SecurityClassService_handler.xml")
public class SecurityClassService
    extends Service
{

    private final static URL SECURITYCLASSSERVICE_WSDL_LOCATION;
    private final static WebServiceException SECURITYCLASSSERVICE_EXCEPTION;
    private final static QName SECURITYCLASSSERVICE_QNAME = new QName("http://securityclass.ellipse.enterpriseservice.mincom.com", "SecurityClassService");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://ews-el8desa.lmnerp03.cerrejon.com/ews/services/SecurityClassService?WSDL");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        SECURITYCLASSSERVICE_WSDL_LOCATION = url;
        SECURITYCLASSSERVICE_EXCEPTION = e;
    }

    public SecurityClassService() {
        super(__getWsdlLocation(), SECURITYCLASSSERVICE_QNAME);
    }

    public SecurityClassService(WebServiceFeature... features) {
        super(__getWsdlLocation(), SECURITYCLASSSERVICE_QNAME, features);
    }

    public SecurityClassService(URL wsdlLocation) {
        super(wsdlLocation, SECURITYCLASSSERVICE_QNAME);
    }

    public SecurityClassService(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, SECURITYCLASSSERVICE_QNAME, features);
    }

    public SecurityClassService(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public SecurityClassService(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns SecurityClass
     */
    @WebEndpoint(name = "SecurityClassServiceHttpPort")
    public SecurityClass getSecurityClassServiceHttpPort() {
        return super.getPort(new QName("http://securityclass.ellipse.enterpriseservice.mincom.com", "SecurityClassServiceHttpPort"), SecurityClass.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link javax.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns SecurityClass
     */
    @WebEndpoint(name = "SecurityClassServiceHttpPort")
    public SecurityClass getSecurityClassServiceHttpPort(WebServiceFeature... features) {
        return super.getPort(new QName("http://securityclass.ellipse.enterpriseservice.mincom.com", "SecurityClassServiceHttpPort"), SecurityClass.class, features);
    }

    private static URL __getWsdlLocation() {
        if (SECURITYCLASSSERVICE_EXCEPTION!= null) {
            throw SECURITYCLASSSERVICE_EXCEPTION;
        }
        return SECURITYCLASSSERVICE_WSDL_LOCATION;
    }

}
