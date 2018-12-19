<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml"
                              xmlns:lu="http://example.com/lookup">

  <xsl:output method="xml" encoding="utf-8" indent="yes"/>

  <xsl:key name="companyById" match="company" use="@id"/>

  <xsl:variable name='matcher-node' select='document("")/xsl:stylesheet/lu:matcher/priorities'/>

  <xsl:template match="/root">
    <root>
      <xsl:apply-templates select="incidents"/>
      <stats>
        <numberOfIncidents>
          <xsl:value-of select="count(/root/incidents/*)"/>
        </numberOfIncidents>
        <!--<reportDate>
          <xsl:value-of  select="current-dateTime()"/>
        </reportDate>-->
        <mostCategory>
        </mostCategory>
      </stats>
    </root>
  </xsl:template>

  <xsl:template match="incidents">
    <incidents>
      <xsl:apply-templates select="incident">
        <xsl:sort select="state"/>
      </xsl:apply-templates>
    </incidents>
  </xsl:template>

  <xsl:template match="incident">
    <xsl:variable name='curr_impact' select='priority/impact'/>
    <xsl:variable name='curr_urgency' select='priority/urgency'/>
    <incident>
      <incidentID>
        <xsl:value-of select="incidentID" />
      </incidentID>
      <endUser>
        <xsl:value-of select="endUser" />
      </endUser>
      <dateOpened>
        <xsl:value-of select="dateOpened" />
      </dateOpened>
      <affectedUsers>
        <xsl:value-of select="affectedUsers" />
      </affectedUsers>
      <state>
        <xsl:value-of select="state" />
      </state>
      <category>
        <mainCategory>
          <xsl:value-of select="category/mainCategory" />
        </mainCategory>
        <subcategory>
          <xsl:value-of select="category/subcategory" />
        </subcategory>
      </category>
      <priority>
        <xsl:value-of select="$matcher-node[@impact=$curr_impact]/@urgency"/>
        <!--<impact>
          <xsl:value-of select="priority/impact" />
        </impact>
        <urgency>
          <xsl:value-of select="$matcher-node[@impact=$curr_impact]/@urgency"/>
          --><!--<xsl:value-of select="$curr_impact"/>--><!--
        </urgency>-->
      </priority>
      <assignmentGroup>
        <xsl:value-of select="assignmentGroup" />
      </assignmentGroup>
      <phoneNumber>
        <xsl:value-of select="phoneNumber" />
      </phoneNumber>
      <userCompany>
        <xsl:value-of select="key('companyById', userCompany/@comid)" />
      </userCompany>
      <shortDescription>
        <xsl:value-of select="shortDescription" />
      </shortDescription>
      <description>
        <xsl:value-of select="description" />
      </description>
      <workNotes>
        <xsl:value-of select="workNotes" />
      </workNotes>
    </incident>
  </xsl:template>

  <!--<xsl:template match="convert">
    <xsl:choose>
      <xsl:when test="priority/impact = Low and priority/urgency = 1 "
    </xsl:choose>
  
  </xsl:template>-->

  <lu:matcher>
    <priorities impact="Low" urgency="1" priority="P3"/>
    <!--<priorities impact="Low" urgency="2" priority="P3"/>
    <priorities impact="Low" urgency="3" priority="P4"/>
    <priorities impact="Low" urgency="4" priority="P4"/>-->
    
    <priorities impact="Medium" urgency="1" priority="P1"/>
    <!--<priorities impact="Medium" urgency="2" priority="P2"/>
    <priorities impact="Medium" urgency="3" priority="P3"/>
    <priorities impact="Medium" urgency="4" priority="P4"/>-->

    <priorities impact="High" urgency="1" priority="P1"/>
    <!--<priorities impact="High" urgency="2" priority="P1"/>
    <priorities impact="High" urgency="3" priority="P2"/>
    <priorities impact="High" urgency="4" priority="P3"/>-->
  </lu:matcher>

</xsl:stylesheet>
